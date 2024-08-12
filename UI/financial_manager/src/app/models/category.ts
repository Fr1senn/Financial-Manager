import { Transaction } from './transaction';
import { User } from './user';

export class Category {
  id: number;
  title: string;
  createdAt: Date;
  transactions?: Transaction[];
  user?: User;

  constructor(id: number, title: string, createdAt: Date = new Date()) {
    this.id = id;
    this.title = title;
    this.createdAt = createdAt;
    this.transactions = [];
    this.user = undefined;
  }
}
