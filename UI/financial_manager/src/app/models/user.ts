import { Category } from './category';
import { Transaction } from './transaction';

export class User {
  id?: number;
  fullName: string;
  email: string;
  password: string;
  registrationDate: Date = new Date();
  monthlyBudget: number = 0;
  budgetUpdateDay: number = 1;
  categories?: Category[];
  transactions?: Transaction[];

  constructor(
    fullName: string,
    email: string,
    password: string,
    monthlyBudget: number = 0,
    budgetUpdateDay: number = 1
  ) {
    this.fullName = fullName;
    this.email = email;
    this.password = password;
    this.monthlyBudget = monthlyBudget;
    this.budgetUpdateDay = budgetUpdateDay;
  }
}
