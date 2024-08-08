import { Injectable } from '@angular/core';
import { Category } from '../../../models/category';


@Injectable({
  providedIn: 'root',
})
export class TransactionCategoryService {
  public getTransactionCategories(): Category[] {
    return transactionCategories;
  }
}
