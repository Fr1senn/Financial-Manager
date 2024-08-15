import { Observable } from 'rxjs';
import { OperationResult } from '../../../../../../models/operation-result';
import { Transaction } from '../../../../../../models/transaction';

export interface ITransactionService {
  getTransactions(
    packSize: number,
    pageNumber: number
  ): Observable<OperationResult<Transaction>>;

  createTransaction(transaction: Transaction): Observable<OperationResult>;

  getTotalTransactionQuantity(userId: number): Observable<object>;

  deleteTransaction(transactionId: number): Observable<OperationResult>;

  updateTransaction(transaction: Transaction): Observable<OperationResult>;
}
