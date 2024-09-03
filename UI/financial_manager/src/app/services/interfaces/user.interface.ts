import { Observable } from 'rxjs';
import { OperationResult } from '../../models/operation-result';
import { User } from '../../models/user';

export interface IUserService {
  getCurrentUserCredentials(): Observable<OperationResult<User>>;
}
