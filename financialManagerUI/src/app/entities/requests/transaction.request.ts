import { BaseEntity } from '../shared/baseEntity';
import { CategoryRequest } from './category.request';

export class TransactionRequest extends BaseEntity {
  public title: string;
  public significance: number;
  public transactionType: string;
  public createdAt?: Date;
  public expenseDate?: Date;
  public category?: CategoryRequest = undefined;

  constructor(
    title: string,
    significance: number,
    transactionType: string,
    expenseDate?: Date,
    category?: CategoryRequest,
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
