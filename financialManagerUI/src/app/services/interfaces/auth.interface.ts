import { Observable } from 'rxjs';

import { RegisterRequest } from '../../entities/requests/register.request';
import { ApiResponse } from '../../entities/shared/apiResponse';
import { LoginRequest } from '../../entities/requests/login.request';
import { TokenResponse } from '../../entities/tokenResponse';

export interface IAuthService {
  register(request: RegisterRequest): Observable<ApiResponse<undefined>>;
  login(request: LoginRequest): Observable<ApiResponse<TokenResponse>>;
  refresh(): Observable<ApiResponse<TokenResponse>>;
  logout(): Observable<ApiResponse<undefined>>;
  isUserAuthenticated(): boolean;
}
