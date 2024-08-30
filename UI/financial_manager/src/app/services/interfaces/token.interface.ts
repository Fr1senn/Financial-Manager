import { TokenResponse } from '../../models/token-response';

export interface ITokenService {
  getAccessToken(): string | null;
  getRefreshToken(): string | null;
  saveTokens(tokenResponse: TokenResponse): void;
  clearTokens(): void;
}
