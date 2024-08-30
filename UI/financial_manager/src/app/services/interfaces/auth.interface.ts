import { BehaviorSubject, Observable } from 'rxjs';
import { LoginModel } from '../../models/login-model';
import { OperationResult } from '../../models/operation-result';
import { TokenResponse } from '../../models/token-response';

export interface IAuthService {
  login(loginModel: LoginModel): Observable<OperationResult<TokenResponse>>;
  refresh(): Observable<OperationResult<TokenResponse>>;
  logout(): void;
  isUserAuthenticated(): boolean;
  isAccessTokenExpired(): boolean;
}
