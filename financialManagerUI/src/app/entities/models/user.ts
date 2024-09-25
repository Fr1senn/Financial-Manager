import { BaseEntity } from '../shared/baseEntity';
import { Category } from './category';
import { Token } from './token';
import { Transaction } from './transaction';

export class User extends BaseEntity {
  public fullName: string;
  public email: string;
  public registrationDate: Date = new Date();
  public monthlyBudget: number;
  public budgetUpdateDay: number;
  public categories?: Category[] = undefined;
  public transactions?: Transaction[] = undefined;
  public tokens?: Token[] = undefined;

  constructor(
    fullName: string,
    email: string,
    monthlyBudget: number = 0,
    budgetUpdateDay: number = 1,
    categories?: Category[],
    transactions?: Transaction[],
    tokens?: Token[],
    id?: number
  ) {
    super();
    this.fullName = fullName;
    this.email = email;
    this.monthlyBudget = monthlyBudget;
    this.budgetUpdateDay = budgetUpdateDay;
    this.categories = categories;
    this.transactions = transactions;
    this.tokens = tokens;
    this.id = id;
  }
}
