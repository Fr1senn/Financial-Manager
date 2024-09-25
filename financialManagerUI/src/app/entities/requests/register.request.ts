export class RegisterRequest {
  public fullName: string;
  public email: string;
  public password: string;
  public passwordConfirm: string;

  constructor(
    fullName: string,
    email: string,
    password: string,
    passwordConfirm: string
  ) {
    this.fullName = fullName;
    this.email = email;
    this.password = password;
    this.passwordConfirm = passwordConfirm;
  }
}
