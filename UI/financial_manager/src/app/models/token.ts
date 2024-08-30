import { User } from './user';

export class Token {
  id?: number;
  refreshToken: string = '';
  isRevoked: boolean = false;
  expirationDate: Date;
  user?: User;

  constructor(refreshToken: string, isRevoked: boolean, expirationDate: Date) {
    this.refreshToken = refreshToken;
    this.isRevoked = isRevoked;
    this.expirationDate = expirationDate;
  }
}
