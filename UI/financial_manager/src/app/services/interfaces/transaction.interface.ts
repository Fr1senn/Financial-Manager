import { Observable } from 'rxjs';
import { OperationResult } from '../../models/operation-result';
import { Transaction } from '../../models/transaction';
import { HttpResponseCode } from '../../models/enums/http-response-code';

export interface ITransactionService {
  getTransactions(
    packSize: number,
    pageNumber: number
  ): Observable<OperationResult<Transaction>>;
  getTotalTransactionQuantity(): Observable<{
    isSuccess: boolean;
    httpResponseCode: HttpResponseCode;
    message?: string;
    data: number;
  }>;
  getMonthlyTransactions(year: number): Observable<{
    isSuccess: boolean;
    httpResponseCode: HttpResponseCode;
    message?: string;
    data: { [month: string]: { income: number; expense: number } };
  }>;
  createTransaction(transaction: Transaction): Observable<OperationResult>;
  deleteTransaction(transactionId: number): Observable<OperationResult>;
  updateTransaction(transaction: Transaction): Observable<OperationResult>;
}
