import { Component, inject, OnInit } from '@angular/core';
import { AuthService } from './services/auth.service';
import { IAuthService } from './services/interfaces/auth.interface';
import { IUserService } from './services/interfaces/user.interface';
import { UserService } from './services/user.service';
import { User } from './models/user';
import { ILoadingService } from './services/interfaces/loading.interface';
import { LoadingService } from './services/loading.service';
import { catchError, delay, Observable } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { UserCredentialsEditingComponent } from './modules/user/components/user-credentials-editing/user-credentials-editing.component';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  public currentUserCredentials: User | undefined;
  public isLoading: Observable<boolean> | undefined;
  public readonly userCredentialsEditing: MatDialog = inject(MatDialog);

  private readonly authService: IAuthService = inject(AuthService);
  private readonly userService: IUserService = inject(UserService);
  private readonly loadingService: ILoadingService = inject(LoadingService);

  public ngOnInit(): void {
    this.isLoading = this.loadingService.loading$;
    if (this.authService.isUserAuthenticated()) {
      this.getCurrentUserCredentials();
    }
  }

  public isUserAuthenticated(): boolean {
    return this.authService.isUserAuthenticated();
  }

  public logout(): void {
    this.authService.logout().subscribe(() => {});
  }

  public openUserCredentialsEditing(): void {
    this.userCredentialsEditing
      .open(UserCredentialsEditingComponent)
      .afterClosed()
      .subscribe(() => {
        this.getCurrentUserCredentials();
      });
  }

  private getCurrentUserCredentials(): void {
    this.loadingService.show();
    this.userService
      .getCurrentUserCredentials()
      .pipe(
        delay(500),
        catchError((err: HttpErrorResponse) => {
          this.logout();
          this.loadingService.hide();
          throw new Error(err.message);
        })
      )
      .subscribe((res) => {
        if (res.isSuccess && res.data) {
          this.currentUserCredentials = res.data[0];
          this.loadingService.hide();
        }
      });
  }
}
