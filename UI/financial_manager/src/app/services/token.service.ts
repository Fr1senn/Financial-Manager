import { inject, Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { ITokenService } from './interfaces/token.interface';
import { TokenResponse } from '../models/token-response';

@Injectable({
  providedIn: 'root',
})
export class TokenService implements ITokenService {
  private readonly cookieService: CookieService = inject(CookieService);

  public getAccessToken(): string | null {
    const accessToken: string | null = this.cookieService.get('accessToken');
    return accessToken;
  }

  public getRefreshToken(): string | null {
    const refreshToken: string | null = this.cookieService.get('refreshToken');
    return refreshToken;
  }

  public saveTokens(tokenResponse: TokenResponse): void {
    this.cookieService.set('accessToken', tokenResponse.accessToken, {
      path: '/',
    });
    this.cookieService.set('refreshToken', tokenResponse.refreshToken, {
      path: '/',
    });
  }

  public clearTokens(): void {
    this.cookieService.deleteAll();
  }
}
