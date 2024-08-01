import { User } from "./user";

export type Category = {
  id: number;
  title: string;
  createdAt: Date;
  userId: number;

  user: User;
};
