import { Observable } from 'rxjs';
import { OperationResult } from '../../../../models/operation-result';
import { Transaction } from '../../../../models/transaction';
import { HttpResponseCode } from '../../../../models/enums/http-response-code';

export interface ITransactionService {
  getTransactions(
    packSize: number,
    pageNumber: number
  ): Observable<OperationResult<Transaction>>;

  getTotalTransactionQuantity(userId: number): Observable<object>;
}
