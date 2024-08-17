import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { provideHttpClient, withFetch } from '@angular/common/http';
import { CategoryModule } from './modules/category/category.module';
import { TransactionModule } from './modules/transaction/transaction.module';

@NgModule({
  declarations: [],
  imports: [CommonModule],
  providers: [provideHttpClient(withFetch())],
  exports: [CategoryModule, TransactionModule],
})
export class TransactionManagementModule {}
