import { WritableSignal } from '@angular/core';

export interface ILoadingService {
  isLoading: WritableSignal<boolean>;

  startLoading(): void;
  stopLoading(): void;
}
