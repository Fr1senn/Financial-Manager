import { inject, Injectable, signal, WritableSignal } from '@angular/core';
import { ITransactionService } from './interfaces/transaction.interface';
import { Observable, tap } from 'rxjs';
import { Transaction } from '../../../entities/models/transaction';
import { PageRequest } from '../../../entities/requests/page.request';
import { ApiResponse } from '../../../entities/shared/apiResponse';
import { API_URL } from '../../../enviroment';
import { HttpClient } from '@angular/common/http';
import { TransactionRequest } from '../../../entities/requests/transaction.request';
import { PaginatedResult } from '../../../entities/shared/paginatedResult';

@Injectable({
  providedIn: 'root',
})
export class TransactionService implements ITransactionService {
  public transactions: WritableSignal<Transaction[] | undefined> = signal<
    Transaction[] | undefined
  >(undefined);
  public transactionsNumber: WritableSignal<number | undefined> = signal<
    number | undefined
  >(undefined);

  private readonly API_URL: string = `${API_URL}/transaction`;
  private readonly _httpClient: HttpClient = inject(HttpClient);

  public getTransactions(
    request: PageRequest
  ): Observable<ApiResponse<PaginatedResult<Transaction[]>>> {
    return this._httpClient
      .post<ApiResponse<PaginatedResult<Transaction[]>>>(
        `${this.API_URL}/all`,
        { ...request }
      )
      .pipe(
        tap((res) => {
          if (res.isSuccess && res.result) {
            this.transactions.set(res.result.data);
            this.transactionsNumber.set(res.result.totalCount);
          }
        })
      );
  }

  public getTransactionById(transactionId: number): Transaction | undefined {
    const transaction: Transaction | undefined = this.transactions()?.find(
      (t) => t.id === transactionId
    );

    return transaction;
  }
  public createTransaction(
    request: TransactionRequest
  ): Observable<ApiResponse<undefined>> {
    return this._httpClient
      .post<ApiResponse<undefined>>(`${this.API_URL}/create`, {
        ...request,
      })
      .pipe(
        tap((res) => {
          if (res.isSuccess) {
            this.getTransactions(new PageRequest(5, 0)).subscribe();
          }
        })
      );
  }

  public deleteTransaction(
    transactionId: number
  ): Observable<ApiResponse<undefined>> {
    return this._httpClient
      .delete<ApiResponse<undefined>>(this.API_URL, {
        params: { transactionId: transactionId },
      })
      .pipe(
        tap((res) => {
          if (res.isSuccess) {
            const currentTransactions = this.transactions();
            this.transactions.set(
              currentTransactions?.filter(
                (transaction) => transaction.id !== transactionId
              )
            );
          }
        })
      );
  }

  public updateTransaction(
    request: TransactionRequest
  ): Observable<ApiResponse<undefined>> {
    return this._httpClient
      .patch<ApiResponse<undefined>>(this.API_URL, { ...request })
      .pipe(
        tap((res) => {
          if (res.isSuccess) {
            this.getTransactions(new PageRequest(5, 0)).subscribe();
          }
        })
      );
  }
}
