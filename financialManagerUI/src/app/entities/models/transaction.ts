import { BaseEntity } from '../shared/baseEntity';
import { Category } from './category';

export class Transaction extends BaseEntity {
  public title: string;
  public significance: number;
  public transactionType: string;
  public createdAt: Date = new Date();
  public expenseDate?: Date;
  public category?: Category = undefined;

  constructor(
    title: string,
    significance: number,
    transactionType: string,
    expenseDate: Date = new Date(),
    category?: Category,
    id?: number
  ) {
    super();
    this.title = title;
    this.significance = significance;
    this.transactionType = transactionType;
    this.expenseDate = expenseDate;
    this.category = category;
    this.id = id;
  }
}
