import { Observable } from 'rxjs';
import { OperationResult } from '../../../../../../models/operation-result';
import { Transaction } from '../../../../../../models/transaction';
import { HttpResponseCode } from '../../../../../../models/enums/http-response-code';

export interface ITransactionService {
  getTransactions(
    packSize: number,
    pageNumber: number
  ): Observable<OperationResult<Transaction>>;

  createTransaction(transaction: Transaction): Observable<OperationResult>;

  getTotalTransactionQuantity(userId: number): Observable<{
    isSucess: boolean;
    httpResponseCode: HttpResponseCode;
    message?: string;
    data: number;
  }>;

  deleteTransaction(transactionId: number): Observable<OperationResult>;

  updateTransaction(transaction: Transaction): Observable<OperationResult>;
}
