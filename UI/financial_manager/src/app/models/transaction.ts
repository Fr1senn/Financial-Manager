import { Category } from './category';
import { User } from './user';

export type Transaction = {
  id: number;
  title: string;
  significance: number;
  transaction_type: string;
  expenseDate: Date;
  createdAt: Date;
  userId: number;
  categoryId: number;

  category: Category;
  user: User;
};
