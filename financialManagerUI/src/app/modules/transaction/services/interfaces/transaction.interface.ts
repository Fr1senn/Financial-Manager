import { WritableSignal } from '@angular/core';
import { Transaction } from '../../../../entities/models/transaction';
import { PageRequest } from '../../../../entities/requests/page.request';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../../../entities/shared/apiResponse';
import { TransactionRequest } from '../../../../entities/requests/transaction.request';
import { PaginatedResult } from '../../../../entities/shared/paginatedResult';

export interface ITransactionService {
  transactions: WritableSignal<Transaction[] | undefined>;
  transactionsNumber: WritableSignal<number | undefined>;

  getTransactions(
    request: PageRequest
  ): Observable<ApiResponse<PaginatedResult<Transaction[]>>>;
  getTransactionById(transactionId: number): Transaction | undefined;
  createTransaction(
    request: TransactionRequest
  ): Observable<ApiResponse<undefined>>;
  deleteTransaction(transactionId: number): Observable<ApiResponse<undefined>>;
  updateTransaction(
    request: TransactionRequest
  ): Observable<ApiResponse<undefined>>;
}
