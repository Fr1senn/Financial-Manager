export type User = {
  id?: number;
  fullName: string;
  email: string;
  registrationDate: Date;
  monthlyBudget: number;
  budgetUpdateDay: number;
  hashedPassword: string;
};