import { Category } from "../../../../../../models/category";

export interface ITransactionCategoriesFilter {
  filterTransactionCategory(
    seekingTransactionCategory: string,
    transactionCategories: Category[]
  ): Category[];
}
