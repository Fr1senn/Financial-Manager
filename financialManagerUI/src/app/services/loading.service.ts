import { Injectable, signal, WritableSignal } from '@angular/core';
import { ILoadingService } from './interfaces/loading.interface';

@Injectable({
  providedIn: 'root',
})
export class LoadingService implements ILoadingService {
  public isLoading: WritableSignal<boolean> = signal<boolean>(false);

  public startLoading(): void {
    this.isLoading.set(true);
  }

  public stopLoading(): void {
    this.isLoading.set(false);
  }
}
