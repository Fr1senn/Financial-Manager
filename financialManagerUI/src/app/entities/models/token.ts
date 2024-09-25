import { BaseEntity } from '../shared/baseEntity';
import { User } from './user';

export class Token extends BaseEntity {
  refreshToken: string;
  expirationDate: Date;
  isRevoked: boolean;
  user?: User = undefined;

  constructor(
    refreshToken: string,
    expirationDate: Date,
    isRevoked: boolean,
    user?: User,
    id?: number
  ) {
    super();
    this.refreshToken = refreshToken;
    this.expirationDate = expirationDate;
    this.isRevoked = isRevoked;
    this.user = user;
    this.id = id;
  }
}
