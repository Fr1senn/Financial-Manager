import { Category } from './category';
import { User } from './user';

export class Transaction {
  id?: number;
  title: string;
  significance: number;
  transactionType: string;
  createdAt?: Date;
  expenseDate?: Date;
  category?: Category;
  user?: User;

  constructor(
    title: string,
    significance: number,
    transactionType: string,
    expenseDate?: Date,
    category?: Category,
    user?: User
  ) {
    this.title = title;
    this.significance = significance;
    this.transactionType = transactionType;
    this.expenseDate = expenseDate;
    this.category = category;
    this.user = user;
  }
}
