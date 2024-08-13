import { Transaction } from './transaction';
import { User } from './user';

export class Category {
  id?: number;
  title: string;
  createdAt?: Date;
  transactions?: Transaction[];
  user?: User;

  constructor(title: string) {
    this.title = title;
    this.transactions = [];
    this.user = undefined;
  }
}
