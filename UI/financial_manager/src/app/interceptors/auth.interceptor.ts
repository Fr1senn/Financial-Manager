import { HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { IAuthService } from '../services/interfaces/auth.interface';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { finalize, switchMap } from 'rxjs';
import { TokenResponse } from '../models/token-response';
import { ITokenService } from '../services/interfaces/token.interface';
import { TokenService } from '../services/token.service';

let isRefreshing: boolean = false;

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService: IAuthService = inject(AuthService);
  const tokenService: ITokenService = inject(TokenService);
  let accessToken: string | null = tokenService.getAccessToken();

  if (accessToken && !authService.isAccessTokenExpired()) {
    return next(addAccessTokenToRequest(req, accessToken));
  } else if (accessToken && authService.isAccessTokenExpired()) {
    if (!isRefreshing) {
      isRefreshing = true;

      return authService.refresh().pipe(
        switchMap((res) => {
          isRefreshing = false;
          if (res.isSuccess && res.data) {
            accessToken = res.data[0].accessToken;
            tokenService.saveTokens(
              new TokenResponse(
                res.data[0].accessToken,
                res.data[0].refreshToken
              )
            );
            return next(addAccessTokenToRequest(req, accessToken));
          } else {
            authService.logout();
            return next(req);
          }
        }),
        finalize(() => {
          isRefreshing = false;
        })
      );
    } else {
      return next(addAccessTokenToRequest(req, accessToken));
    }
  } else {
    return next(req);
  }
};

const addAccessTokenToRequest = (
  req: HttpRequest<any>,
  accessToken: string
) => {
  return req.clone({ setHeaders: { Authorization: `Bearer ${accessToken}` } });
};
