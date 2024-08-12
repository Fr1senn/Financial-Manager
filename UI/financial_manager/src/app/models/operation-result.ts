import { Category } from './category';
import { HttpResponseCode } from './enums/http-response-code';
import { Transaction } from './transaction';
import { User } from './user';

export class OperationResult<T = User | Category | Transaction> {
  isSuccess: boolean;
  httpResponseCode: HttpResponseCode;
  message?: string;
  data?: T[];

  constructor(
    isSuccess: boolean,
    httpResponseCode: HttpResponseCode,
    message?: string,
    data?: T[]
  ) {
    this.isSuccess = isSuccess;
    this.httpResponseCode = httpResponseCode;
    this.message = message;
    this.data = data;
  }
}
