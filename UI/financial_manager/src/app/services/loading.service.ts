import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { ILoadingService } from './interfaces/loading.interface';

@Injectable({
  providedIn: 'root',
})
export class LoadingService implements ILoadingService {
  private loadingSubject = new BehaviorSubject<boolean>(false);
  public loading$ = this.loadingSubject.asObservable();

  public show(): void {
    this.loadingSubject.next(true);
  }

  public hide(): void {
    this.loadingSubject.next(false);
  }
}
