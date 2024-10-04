import { WritableSignal } from '@angular/core';
import { Transaction } from '../../../../entities/models/transaction';
import { PageRequest } from '../../../../entities/requests/page.request';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../../../entities/shared/apiResponse';
import { TransactionRequest } from '../../../../entities/requests/transaction.request';

export interface ITransactionService {
  transactions: WritableSignal<Transaction[] | undefined>;

  getTransactions(request: PageRequest): Observable<ApiResponse<Transaction[]>>;
  createTransaction(
    request: TransactionRequest
  ): Observable<ApiResponse<undefined>>;
  deleteTransaction(transactionId: number): Observable<ApiResponse<undefined>>;
  updateTransaction(
    request: TransactionRequest
  ): Observable<ApiResponse<undefined>>;
}
