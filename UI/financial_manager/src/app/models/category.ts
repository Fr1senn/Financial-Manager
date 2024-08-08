import { User } from "./user";

export type Category = {
  id?: number;
  title: string;
  createdAt: Date;

  user?: User;
};
