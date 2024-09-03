import { Component, inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { User } from '../../../../models/user';
import { IUserService } from '../../../../services/interfaces/user.interface';
import { UserService } from '../../../../services/user.service';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-user-credentials-editing',
  templateUrl: './user-credentials-editing.component.html',
  styleUrl: './user-credentials-editing.component.css',
})
export class UserCredentialsEditingComponent implements OnInit {
  public currentUserCredentials: User | undefined;
  public userCredentialsEditingForm: FormGroup | undefined;

  private dialogRef: MatDialogRef<UserCredentialsEditingComponent> = inject(
    MatDialogRef<UserCredentialsEditingComponent>
  );
  private readonly userService: IUserService = inject(UserService);

  public ngOnInit(): void {
    this.getCurrentUserCredentials();
  }

  public updateUserCredentials(): void {
    const user: User = new User(
      this.userCredentialsEditingForm?.value.fullName,
      '',
      '',
      this.userCredentialsEditingForm?.value.monthlyBudget,
      this.userCredentialsEditingForm?.value.budgetUpdateDay
    );

    this.userService.updateUserCredentials(user).subscribe((res) => {
      if (res.isSuccess) {
        this.dialogRef.close();
      }
    });
  }

  private getCurrentUserCredentials(): void {
    this.userService.getCurrentUserCredentials().subscribe((res) => {
      if (res.isSuccess && res.data) {
        this.currentUserCredentials = res.data[0];
        this.userCredentialsEditingForm =
          this.createUserCredentialsEditingForm();
      }
    });
  }

  private createUserCredentialsEditingForm(): FormGroup {
    return new FormGroup({
      fullName: new FormControl(this.currentUserCredentials?.fullName, [
        Validators.required,
        Validators.minLength(8),
      ]),
      monthlyBudget: new FormControl(
        this.currentUserCredentials?.monthlyBudget,
        [Validators.min(0)]
      ),
      budgetUpdateDay: new FormControl(
        this.currentUserCredentials?.budgetUpdateDay,
        [Validators.min(1), Validators.max(31)]
      ),
    });
  }
}
