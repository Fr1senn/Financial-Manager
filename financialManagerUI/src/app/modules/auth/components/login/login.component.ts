import { Component, inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IAuthService } from '../../../../services/interfaces/auth.interface';
import { AuthService } from '../../../../services/auth.service';
import { LoginRequest } from '../../../../entities/requests/login.request';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { catchError, of, tap } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent implements OnInit {
  public loginForm: FormGroup | undefined;
  public errorMessage: string = '';

  private readonly _authService: IAuthService = inject(AuthService);
  private readonly _router: Router = inject(Router);

  public ngOnInit(): void {
    this.loginForm = this.createLoginForm();
  }

  public login(): void {
    const request: LoginRequest = new LoginRequest(
      this.loginForm?.value.email,
      this.loginForm?.value.password
    );

    this._authService
      .login(request)
      .pipe(
        tap((res) => {
          if (res.isSuccess) {
            this._router.navigate(['home']);
          }
        }),
        catchError((error: HttpErrorResponse) => {
          this.errorMessage = error.error.message;
          return of(null);
        })
      )
      .subscribe();
  }

  private createLoginForm(): FormGroup {
    return new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [
        Validators.required,
        Validators.minLength(12),
      ]),
    });
  }
}
