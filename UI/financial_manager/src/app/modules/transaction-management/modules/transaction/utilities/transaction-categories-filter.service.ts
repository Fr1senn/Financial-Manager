import { Injectable } from '@angular/core';
import { ITransactionCategoriesFilter } from './interfaces/ITransactionCategoriesFilter';
import { Category } from '../../../../../models/category';

@Injectable({
  providedIn: 'root',
})
export class TransactionCategoriesFilterService
  implements ITransactionCategoriesFilter
{
  filterTransactionCategory(
    seekingTransactionCategory: string,
    transactionCategories: Category[]
  ): Category[] {
    if (seekingTransactionCategory === '') {
      return transactionCategories;
    } else {
      return transactionCategories.filter((transactionCategory) =>
        transactionCategory.title
          .toLowerCase()
          .includes(seekingTransactionCategory)
      );
    }
  }
}
