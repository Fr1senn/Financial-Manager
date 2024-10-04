import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatSelectModule } from '@angular/material/select';

import { TransacionLayoutComponent } from './components/transacion-layout/transacion-layout.component';
import { TransactionsComponent } from './components/transactions/transactions.component';
import { TransactionItemComponent } from './components/transaction-item/transaction-item.component';
import { TransactionCreationComponent } from './components/transaction-creation/transaction-creation.component';
import { ReactiveFormsModule } from '@angular/forms';
import { CategoryCreationComponent } from './components/category-creation/category-creation.component';
import { TransactionEditingComponent } from './components/transaction-editing/transaction-editing.component';

@NgModule({
  declarations: [
    TransacionLayoutComponent,
    TransactionsComponent,
    TransactionItemComponent,
    TransactionCreationComponent,
    CategoryCreationComponent,
    TransactionEditingComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatIconModule,
    MatButtonModule,
    MatPaginatorModule,
    MatDialogModule,
    MatInputModule,
    MatFormFieldModule,
    MatDatepickerModule,
    MatSelectModule,
  ],
})
export class TransactionModule {}
