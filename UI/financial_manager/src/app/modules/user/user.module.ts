import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { provideHttpClient, withFetch } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';

import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

import { UserBudgetInfoComponent } from './components/user-budget-info/user-budget-info.component';
import { UserCredentialsEditingComponent } from './components/user-credentials-editing/user-credentials-editing.component';

@NgModule({
  declarations: [UserBudgetInfoComponent, UserCredentialsEditingComponent],
  imports: [
    CommonModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatDialogModule,
    ReactiveFormsModule,
  ],
  exports: [UserCredentialsEditingComponent],
  providers: [provideHttpClient(withFetch())],
})
export class UserModule {}
