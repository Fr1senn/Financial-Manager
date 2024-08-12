import { Category } from './category';

export class Transaction {
  id?: number;
  title: string;
  significance: number;
  createdAt: Date = new Date();
  expenseDate?: Date;
  category?: Category;

  private _transactionType: string;
  public get transactionType(): string {
    return this._transactionType;
  }

  public set transactionType(value: string) {
    if (value !== 'income' && value !== 'expense') {
      throw new Error("Transaction type must be either 'income' or 'expense'.");
    }
    this._transactionType = value;
  }

  constructor(
    title: string,
    significance: number,
    transactionType: string,
    expenseDate?: Date
  ) {
    this.title = title;
    this.significance = significance;
    this._transactionType = transactionType;
    this.expenseDate = expenseDate;
  }
}
