import { Category } from './category';
import { Transaction } from './transaction';

export class User {
  id: number;
  fullName: string;
  email: string;
  registrationDate: Date;
  monthlyBudget: number;
  categories: Category[];
  transactions: Transaction[];

  private _budgetUpdateDay: number;
  private hashedPassword: string;

  constructor(
    id: number,
    fullName: string,
    email: string,
    registrationDate: Date = new Date(),
    monthlyBudget: number = 0
  ) {
    this.id = id;
    this.fullName = fullName;
    this.email = email;
    this.registrationDate = registrationDate;
    this.monthlyBudget = monthlyBudget;
    this.categories = [];
    this.transactions = [];
    this._budgetUpdateDay = 1; // default value
    this.hashedPassword = '';
  }

  get budgetUpdateDay(): number {
    return this._budgetUpdateDay;
  }

  set budgetUpdateDay(value: number) {
    if (value < 1 || value > 31) {
      throw new Error('Budget update day should be in range from 1 to 31');
    }
    this._budgetUpdateDay = value;
  }
}
