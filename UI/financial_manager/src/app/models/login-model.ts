export class LoginModel {
  email: string = '';
  rawPassword: string = '';

  constructor(email:string,rawPassword:string) {
    this.email = email;
    this.rawPassword = rawPassword;
  }
}
