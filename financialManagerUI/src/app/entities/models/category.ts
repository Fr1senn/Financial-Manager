import { BaseEntity } from '../shared/baseEntity';
import { Transaction } from './transaction';
import { User } from './user';

export class Category extends BaseEntity {
  public title: string;
  public createdAt: Date = new Date();
  public user?: User = undefined;
  public transactions?: Transaction[] = undefined;

  constructor(
    title: string,
    user?: User,
    transactions?: Transaction[],
    id?: number
  ) {
    super();
    this.title = title;
    this.user = user;
    this.transactions = transactions;
    this.id = id;
  }
}
