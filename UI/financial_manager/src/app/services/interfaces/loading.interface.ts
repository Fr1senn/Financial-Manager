import { Observable } from 'rxjs';

export interface ILoadingService {
  loading$: Observable<boolean>;

  show(): void;
  hide(): void;
}
