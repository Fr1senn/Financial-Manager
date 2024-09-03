import { Observable } from 'rxjs';
import { LoginModel } from '../../models/login-model';
import { OperationResult } from '../../models/operation-result';
import { TokenResponse } from '../../models/token-response';
import { User } from '../../models/user';

export interface IAuthService {
  login(loginModel: LoginModel): Observable<OperationResult<TokenResponse>>;
  register(user: User): Observable<OperationResult>;
  refresh(): Observable<OperationResult<TokenResponse>>;
  logout(): void;
  isUserAuthenticated(): boolean;
  isAccessTokenExpired(): boolean;
}
