import { Category } from './category';
import { User } from './user';

export type Transaction = {
  id?: number;
  title: string;
  significance: number;
  transactionType: string;
  expenseDate?: Date;
  createdAt?: Date;

  category?: Category;
  user?: User;
};
