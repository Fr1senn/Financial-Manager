import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TransactionComponent } from './components/transaction/transaction.component';
import { TransactionsComponent } from './components/transactions/transactions.component';
import { TransactionCreationComponent } from './components/transaction-creation/transaction-creation.component';

@NgModule({
  declarations: [
    TransactionComponent,
    TransactionsComponent,
    TransactionCreationComponent,
  ],
  imports: [
    CommonModule,
  ],
  exports: [TransactionsComponent],
})
export class TransactionModule {}
