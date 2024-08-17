import { Injectable } from '@angular/core';
import { ITransactionDateFormatter } from './interfaces/ITransactionDateFormatter';
import { DateTime } from 'luxon';

@Injectable({
  providedIn: 'root',
})
export class TransactionDateService implements ITransactionDateFormatter {
  public formatDate(dateTime: string): string {
    return DateTime.fromISO(dateTime).toLocaleString(DateTime.DATETIME_SHORT);
  }
}
