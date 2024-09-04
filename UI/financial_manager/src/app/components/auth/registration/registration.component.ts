import { Component, inject, OnInit } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { IAuthService } from '../../../services/interfaces/auth.interface';
import { AuthService } from '../../../services/auth.service';
import { User } from '../../../models/user';
import { LoginModel } from '../../../models/login-model';

@Component({
  selector: 'app-registration',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatInputModule,
    MatButtonModule,
    MatFormFieldModule,
  ],
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.css',
})
export class RegistrationComponent implements OnInit {
  public registrationForm: FormGroup | undefined;

  private readonly authService: IAuthService = inject(AuthService);

  public ngOnInit(): void {
    this.registrationForm = this.createRegistrationForm();
  }

  public register(): void {
    const user: User = new User(
      this.registrationForm?.value.fullName,
      this.registrationForm?.value.email,
      this.registrationForm?.value.password
    );

    this.authService.register(user).subscribe((res) => {
      if (res.isSuccess) {
        this.authService
          .login(
            new LoginModel(
              this.registrationForm?.value.email,
              this.registrationForm?.value.password
            )
          )
          .subscribe();
      }
    });
  }

  private createRegistrationForm(): FormGroup {
    return new FormGroup({
      fullName: new FormControl('', [
        Validators.required,
        Validators.minLength(8),
      ]),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', Validators.required),
    });
  }
}
