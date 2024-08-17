import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { provideHttpClient, withFetch } from '@angular/common/http';

import { UserBudgetInfoComponent } from './components/user-budget-info/user-budget-info.component';

import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@NgModule({
  declarations: [UserBudgetInfoComponent],
  imports: [CommonModule, MatButtonModule, MatIconModule],
  exports: [UserBudgetInfoComponent],
  providers: [provideHttpClient(withFetch())],
})
export class UserModule {}
