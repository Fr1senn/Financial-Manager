import { Component, inject, OnInit, WritableSignal } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { IUserService } from '../../services/interfaces/user.interface';
import { UserService } from '../../services/user.service';
import { User } from '../../../../entities/models/user';
import { UserRequest } from '../../../../entities/requests/user.request';
import { catchError, tap, throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-user-credentials-editing',
  templateUrl: './user-credentials-editing.component.html',
  styleUrl: './user-credentials-editing.component.css',
})
export class UserCredentialsEditingComponent implements OnInit {
  private readonly _dialogRef: MatDialogRef<UserCredentialsEditingComponent> =
    inject(MatDialogRef<UserCredentialsEditingComponent>);
  private readonly _userService: IUserService = inject(UserService);

  public userCredentialsEditingForm: FormGroup | undefined;
  public userCredentials: WritableSignal<User | null> =
    this._userService.userCredentials;
  public errorMessage: string = '';

  public ngOnInit(): void {
    this.userCredentialsEditingForm = this.createUserCredentialsEditingForm();
  }

  public updateUserCredentials(): void {
    const request: UserRequest = new UserRequest(
      this.userCredentialsEditingForm?.value.fullName,
      this.userCredentialsEditingForm?.value.email,
      this.userCredentialsEditingForm?.value.monthlyBudget,
      this.userCredentialsEditingForm?.value.budgetUpdateDay
    );

    this._userService
      .updateUserCredentials(request)
      .pipe(
        tap((res) => {
          if (res.isSuccess) {
            this._dialogRef.close();
          }
        }),
        catchError((error: HttpErrorResponse) => {
          this.errorMessage = error.error.message;
          return throwError(() => error);
        })
      )
      .subscribe();
  }

  public closeDialog(): void {
    this._dialogRef.close();
  }

  private createUserCredentialsEditingForm(): FormGroup {
    return new FormGroup({
      fullName: new FormControl(this.userCredentials()?.fullName, [
        Validators.required,
        Validators.minLength(8),
      ]),
      email: new FormControl(this.userCredentials()?.email, [
        Validators.required,
        Validators.email,
      ]),
      monthlyBudget: new FormControl(this.userCredentials()?.monthlyBudget, [
        Validators.min(0),
      ]),
      budgetUpdateDay: new FormControl(
        this.userCredentials()?.budgetUpdateDay,
        [Validators.min(1), Validators.max(31)]
      ),
    });
  }
}
