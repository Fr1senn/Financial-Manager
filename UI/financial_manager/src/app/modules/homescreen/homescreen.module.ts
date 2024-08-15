import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TransactionModule } from './modules/transaction/transaction.module';
import { CategoryModule } from './modules/category/category.module';

@NgModule({
  declarations: [],
  imports: [CommonModule],
  exports: [TransactionModule, CategoryModule],
})
export class HomescreenModule {}
