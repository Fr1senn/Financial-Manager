import { Category } from "./category";
import { User } from "./user";

export type Transaction = {
  id: number;
  title: string;
  significance: number;
  expenseDate: Date;
  createdAt: Date;
  userId: number;
  categoryId: number;

  category: Category;
  user: User;
};