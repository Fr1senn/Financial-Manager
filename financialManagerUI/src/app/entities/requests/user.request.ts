export class UserRequest {
  public fullName: string;
  public email: string;
  public monthlyBudget: number;
  public budgetUpdateDay: number;

  constructor(
    fullName: string,
    email: string,
    monthlyBudget: number = 0,
    budgetUpdateDay: number = 1
  ) {
    this.fullName = fullName;
    this.email = email;
    this.monthlyBudget = monthlyBudget;
    this.budgetUpdateDay = budgetUpdateDay;
  }
}
