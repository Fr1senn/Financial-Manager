import { inject, Injectable } from '@angular/core';

import { CookieService } from 'ngx-cookie-service';
import { jwtDecode } from 'jwt-decode';

import { ITokenService } from './interfaces/token.interface';
import { TokenResponse } from '../entities/tokenResponse';

@Injectable({
  providedIn: 'root',
})
export class TokenService implements ITokenService {
  private readonly _cookieService: CookieService = inject(CookieService);

  public getTokens(): TokenResponse {
    const accessToken: string | null = this._cookieService.get('accessToken');
    const refreshToken: string | null = this._cookieService.get('refreshToken');
    return {
      accessToken: accessToken,
      refreshToken: refreshToken,
    };
  }

  public saveTokens(tokens: TokenResponse): void {
    this._cookieService.set('accessToken', tokens.accessToken, { path: '/' });
    this._cookieService.set('refreshToken', tokens.refreshToken, { path: '/' });
  }

  public clearTokens(): void {
    this._cookieService.delete('accessToken');
    this._cookieService.delete('refreshToken');
  }

  public isAccessTokenExpired(): boolean {
    const accessToken: string | null = this.getTokens().accessToken;

    if (!accessToken) {
      return true;
    }

    try {
      const decodedToken: any = jwtDecode(accessToken);
      const expirationTime = decodedToken.exp;
      const currentTime = Math.floor(Date.now() / 1000);

      return expirationTime <= currentTime;
    } catch (error) {
      console.error(error);
      return true;
    }
  }
}
