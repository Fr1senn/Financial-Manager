import { Observable } from 'rxjs';

import { WritableSignal } from '@angular/core';
import { User } from '../../../../entities/models/user';
import { ApiResponse } from '../../../../entities/shared/apiResponse';
import { UserRequest } from '../../../../entities/requests/user.request';

export interface IUserService {
  userCredentials: WritableSignal<User | null>;
  getUserCredentials(): Observable<ApiResponse<User>>;
  updateUserCredentials(request: UserRequest): Observable<ApiResponse<undefined>>;
}
