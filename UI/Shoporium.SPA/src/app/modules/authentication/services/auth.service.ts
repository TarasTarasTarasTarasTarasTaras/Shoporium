import { Injectable, Injector } from '@angular/core';
import { LoginResult } from '../models/login-result';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private injector: Injector) { }


  updateCurrentUser(x: LoginResult) {
    this.setLocalStorage(x);
  }

  private setLocalStorage(x: LoginResult) {
    localStorage.setItem('access_token', x.accessToken);
    localStorage.setItem('refresh_token', x.refreshToken);
    localStorage.setItem('login-event', 'login' + Math.random());
  }

}
