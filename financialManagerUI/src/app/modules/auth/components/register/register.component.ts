import { Component, inject, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  ValidationErrors,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { IAuthService } from '../../../../services/interfaces/auth.interface';
import { AuthService } from '../../../../services/auth.service';
import { RegisterRequest } from '../../../../entities/requests/register.request';
import { catchError, of, tap } from 'rxjs';
import { LoginRequest } from '../../../../entities/requests/login.request';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent implements OnInit {
  public registerForm: FormGroup | undefined;
  public errorMessage: string = '';

  private readonly _authService: IAuthService = inject(AuthService);
  private readonly _router: Router = inject(Router);

  public ngOnInit(): void {
    this.registerForm = this.createRegisterForm();
  }

  public register(): void {
    const request: RegisterRequest = new RegisterRequest(
      this.registerForm?.value.fullName,
      this.registerForm?.value.email,
      this.registerForm?.value.password,
      this.registerForm?.value.passwordConfirm
    );

    this._authService
      .register(request)
      .pipe(
        tap((res) => {
          if (res.isSuccess) {
            this._authService
              .login(new LoginRequest(request.email, request.password))
              .pipe(
                tap((res) => {
                  if (res.isSuccess) {
                    this._router.navigate(['home']);
                  }
                })
              )
              .subscribe();
          }
        }),
        catchError((error: HttpErrorResponse) => {
          this.errorMessage = error.error.message;
          return of(null);
        })
      )
      .subscribe();
  }

  private createRegisterForm(): FormGroup {
    return new FormGroup(
      {
        fullName: new FormControl('', [
          Validators.required,
          Validators.minLength(8),
        ]),
        email: new FormControl('', [Validators.required, Validators.email]),
        password: new FormControl('', [
          Validators.required,
          Validators.minLength(12),
        ]),
        passwordConfirm: new FormControl('', [
          Validators.required,
          Validators.minLength(12),
        ]),
      },
      {
        validators: this.passwordsMatchValidator('password', 'passwordConfirm'),
      }
    );
  }

  private passwordsMatchValidator(source: string, target: string): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const sourceCtrl = control.get(source);
      const targetCtrl = control.get(target);

      return sourceCtrl && targetCtrl && sourceCtrl.value !== targetCtrl.value
        ? { mismatch: true }
        : null;
    };
  }
}
