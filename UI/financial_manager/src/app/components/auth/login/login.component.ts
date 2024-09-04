import { Component, inject, OnInit } from '@angular/core';

import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';

import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { IAuthService } from '../../../services/interfaces/auth.interface';
import { AuthService } from '../../../services/auth.service';
import { LoginModel } from '../../../models/login-model';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatInputModule,
    MatButtonModule,
    MatFormFieldModule,
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent implements OnInit {
  public loginForm: FormGroup | undefined;

  private readonly authService: IAuthService = inject(AuthService);

  ngOnInit(): void {
    this.loginForm = this.createLoginForm();
  }

  public login(): void {
    const loginModel = new LoginModel(
      this.loginForm?.value.email,
      this.loginForm?.value.rawPassword
    );
    this.authService.login(loginModel).subscribe();
  }

  private createLoginForm(): FormGroup {
    return new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      rawPassword: new FormControl('', [Validators.required]),
    });
  }
}
