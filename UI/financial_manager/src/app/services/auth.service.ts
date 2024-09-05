import { inject, Injectable } from '@angular/core';
import { enviroment } from '../enviroment';
import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from '@angular/common/http';
import { IAuthService } from './interfaces/auth.interface';
import { LoginModel } from '../models/login-model';
import { OperationResult } from '../models/operation-result';
import { TokenResponse } from '../models/token-response';
import { catchError, finalize, Observable, tap, throwError } from 'rxjs';
import { jwtDecode } from 'jwt-decode';
import { ITokenService } from './interfaces/token.interface';
import { TokenService } from './token.service';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root',
})
export class AuthService implements IAuthService {
  private readonly baseApiUrl: string = enviroment.baseApiUrl;
  private readonly httpClient: HttpClient = inject(HttpClient);
  private readonly tokenService: ITokenService = inject(TokenService);

  public login(
    loginModel: LoginModel
  ): Observable<OperationResult<TokenResponse>> {
    return this.httpClient
      .post<OperationResult<TokenResponse>>(`${this.baseApiUrl}/Auth/Login`, {
        ...loginModel,
      })
      .pipe(
        tap((res: OperationResult<TokenResponse>) => {
          if (res.isSuccess && res.data) {
            this.tokenService.saveTokens(
              new TokenResponse(
                res.data[0].accessToken,
                res.data[0].refreshToken
              )
            );
          }
        }),
        catchError((err: HttpErrorResponse) => {
          return throwError(err);
        })
      );
  }

  public register(user: User): Observable<OperationResult> {
    return this.httpClient.post<OperationResult>(
      `${this.baseApiUrl}/Auth/Register`,
      { ...user }
    );
  }

  public refresh(): Observable<OperationResult<TokenResponse>> {
    const refreshToken = this.tokenService.getRefreshToken();
    return this.httpClient
      .post<OperationResult<TokenResponse>>(
        `${this.baseApiUrl}/Auth/Refresh`,
        { refreshToken },
        {
          headers: new HttpHeaders({
            'Content-Type': 'application/json',
          }),
        }
      )
      .pipe(
        tap((res: OperationResult<TokenResponse>) => {
          if (res.isSuccess && res.data) {
            this.tokenService.saveTokens(
              new TokenResponse(
                res.data[0].accessToken,
                res.data[0].refreshToken
              )
            );
          }
        }),
        catchError((error) => {
          return throwError(error);
        })
      );
  }

  public logout(): Observable<OperationResult> {
    const refreshToken = this.tokenService.getRefreshToken();
    return this.httpClient
      .post<OperationResult>(
        `${this.baseApiUrl}/Auth/Logout`,
        { refreshToken },
        {
          headers: new HttpHeaders({
            'Content-Type': 'application/json',
          }),
        }
      )
      .pipe(
        finalize(() => {
          this.tokenService.clearTokens();
        })
      );
  }

  public isUserAuthenticated(): boolean {
    const accessToken: string | null = this.tokenService.getAccessToken();
    return !!accessToken;
  }

  public isAccessTokenExpired(): boolean {
    const accessToken: string | null = this.tokenService.getAccessToken();

    if (!accessToken) {
      return true;
    }

    try {
      const decodedToken: any = jwtDecode(accessToken);
      const expirationTime = decodedToken.exp;
      const currentTime = Math.floor(Date.now() / 1000);

      return expirationTime <= currentTime;
    } catch (error) {
      console.error('Error decoding token:', error);
      return true;
    }
  }
}
