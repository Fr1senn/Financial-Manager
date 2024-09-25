import { Component, inject, OnInit, WritableSignal } from '@angular/core';
import { IAuthService } from '../../../../services/interfaces/auth.interface';
import { AuthService } from '../../../../services/auth.service';
import { Router } from '@angular/router';
import { IUserService } from '../../services/interfaces/user.interface';
import { UserService } from '../../services/user.service';
import { User } from '../../../../entities/models/user';
import { MatDialog } from '@angular/material/dialog';
import { UserCredentialsEditingComponent } from '../user-credentials-editing/user-credentials-editing.component';
import { catchError, tap, throwError } from 'rxjs';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrl: './navigation.component.css',
})
export class NavigationComponent implements OnInit {
  private readonly _authService: IAuthService = inject(AuthService);
  private readonly _userService: IUserService = inject(UserService);
  private readonly _router: Router = inject(Router);
  private readonly _dialog: MatDialog = inject(MatDialog);

  public userCredentials: WritableSignal<User | null> =
    this._userService.userCredentials;

  public ngOnInit(): void {
    this.getUserCredentials();
  }

  public openUserCredentialsEditing(): void {
    const dialogRef = this._dialog.open(UserCredentialsEditingComponent);
  }

  public logout(): void {
    this._authService
      .logout()
      .pipe(
        tap((res) => {
          if (res.isSuccess) {
            this._router.navigate(['login']);
          }
        }),
        catchError((error) => throwError(() => error))
      )
      .subscribe();
  }

  private getUserCredentials(): void {
    this._userService.getUserCredentials().subscribe();
  }
}
