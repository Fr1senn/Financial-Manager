import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TransactionModule } from './modules/transaction/transaction.module';

@NgModule({
  declarations: [],
  imports: [CommonModule, TransactionModule],
  exports: [TransactionModule]
})
export class HomescreenModule {}
