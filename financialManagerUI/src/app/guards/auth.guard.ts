import { inject, Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  GuardResult,
  MaybeAsync,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { catchError, delay, of, switchMap, throwError } from 'rxjs';
import { IAuthService } from '../services/interfaces/auth.interface';
import { AuthService } from '../services/auth.service';
import { LoadingService } from '../services/loading.service';
import { ILoadingService } from '../services/interfaces/loading.interface';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  private readonly _authService: IAuthService = inject(AuthService);
  private readonly _router: Router = inject(Router);
  private readonly _loadingService: ILoadingService = inject(LoadingService);

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): MaybeAsync<GuardResult> {
    if (this._authService.isUserAuthenticated()) {
      return of(true);
    }

    this._loadingService.startLoading();
    return this._authService.refresh().pipe(
      delay(1000),
      switchMap((res) => {
        if (res.isSuccess) {
          this._loadingService.stopLoading();
          return of(true);
        } else {
          return this._authService.logout().pipe(
            switchMap((res) => {
              if (res.isSuccess) {
                this._loadingService.stopLoading();
                this._router.navigate(['login']);
                return of(false);
              }
              this._loadingService.stopLoading();
              return of(false);
            })
          );
        }
      }),
      catchError((error) => {
        this._loadingService.stopLoading();
        this._router.navigate(['login']);
        return throwError(() => error);
      })
    );
  }
}
