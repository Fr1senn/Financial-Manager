import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, tap } from 'rxjs';

import { IAuthService } from './interfaces/auth.interface';
import { LoginRequest } from '../entities/requests/login.request';
import { RegisterRequest } from '../entities/requests/register.request';
import { API_URL } from '../enviroment';
import { ApiResponse } from '../entities/shared/apiResponse';
import { TokenResponse } from '../entities/tokenResponse';
import { ITokenService } from './interfaces/token.interface';
import { TokenService } from './token.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService implements IAuthService {
  private readonly API_URL: string = `${API_URL}/Auth`;
  private readonly _httpClient: HttpClient = inject(HttpClient);
  private readonly _tokenService: ITokenService = inject(TokenService);

  public register(
    request: RegisterRequest
  ): Observable<ApiResponse<undefined>> {
    return this._httpClient.post<ApiResponse<undefined>>(
      `${this.API_URL}/Register`,
      { ...request }
    );
  }

  public login(request: LoginRequest): Observable<ApiResponse<TokenResponse>> {
    return this._httpClient
      .post<ApiResponse<TokenResponse>>(`${this.API_URL}/Login`, { ...request })
      .pipe(
        tap((res) => {
          if (res.isSuccess && res.result) {
            this._tokenService.saveTokens(res.result);
          }
        })
      );
  }

  public refresh(): Observable<ApiResponse<TokenResponse>> {
    const refreshToken: string = this._tokenService.getTokens().refreshToken;
    return this._httpClient
      .post<ApiResponse<TokenResponse>>(
        `${this.API_URL}/Refresh`,
        { refreshToken },
        {
          headers: new HttpHeaders({
            'Content-Type': 'application/json',
          }),
        }
      )
      .pipe(
        tap((res) => {
          if (res.isSuccess && res.result) {
            this._tokenService.clearTokens();
            this._tokenService.saveTokens(res.result);
          }
        })
      );
  }

  public logout(): Observable<ApiResponse<undefined>> {
    const refreshToken: string = this._tokenService.getTokens().refreshToken;
    return this._httpClient
      .post<ApiResponse<undefined>>(
        `${this.API_URL}/Logout`,
        { refreshToken },
        {
          headers: new HttpHeaders({
            'Content-Type': 'application/json',
          }),
        }
      )
      .pipe(
        tap((res) => {
          if (res.isSuccess) {
            console.log('service');
            this._tokenService.clearTokens();
          }
        })
      );
  }

  public isUserAuthenticated(): boolean {
    const accessToken: string = this._tokenService.getTokens().accessToken;
    return !!(accessToken && !this._tokenService.isAccessTokenExpired());
  }
}
