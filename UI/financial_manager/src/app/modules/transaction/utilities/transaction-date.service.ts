import { Injectable } from '@angular/core';
import { ITransactionDateFormatter } from './interfaces/ITransactionDateFormatter';
import { DateTime } from 'luxon';

@Injectable({
  providedIn: 'root',
})
export class TransactionDateService implements ITransactionDateFormatter {
  public formatDate(date: Date): string {
    return DateTime.fromJSDate(date).toLocaleString(DateTime.DATETIME_SHORT);
  }
}
