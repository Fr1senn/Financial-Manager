import { inject, Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
} from '@angular/common/http';
import { catchError, Observable, switchMap, throwError } from 'rxjs';

import { IAuthService } from '../services/interfaces/auth.interface';
import { AuthService } from '../services/auth.service';
import { ITokenService } from '../services/interfaces/token.interface';
import { TokenService } from '../services/token.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  private readonly _authService: IAuthService = inject(AuthService);
  private readonly _tokenService: ITokenService = inject(TokenService);

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    const accessToken = this._tokenService.getTokens().accessToken;

    if (this._authService.isUserAuthenticated()) {
      request = this.addTokenToRequest(request, accessToken);
    }

    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.status === 0) {
          return this._authService.refresh().pipe(
            switchMap((res) => {
              if (res.isSuccess && res.result) {
                request = this.addTokenToRequest(
                  request,
                  res.result?.accessToken
                );
                return next.handle(request);
              } else {
                return next.handle(request);
              }
            })
          );
        }
        return throwError(() => error);
      })
    );
  }

  private addTokenToRequest(req: HttpRequest<any>, token: string) {
    return req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`,
      },
    });
  }
}
