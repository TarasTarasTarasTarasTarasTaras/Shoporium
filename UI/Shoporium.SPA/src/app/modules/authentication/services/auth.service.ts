import { Injectable, Injector } from '@angular/core';
import { AgentCTKService } from '../../core/services/ctk.service';
import { LoginResult } from '../models/login-result';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private injector: Injector) { }


  updateCurrentUser(x: LoginResult) {
    this.setLocalStorage(x);

    const agentCtkService = this.injector.get(AgentCTKService);
    agentCtkService.resetCTK();
  }

  private setLocalStorage(x: LoginResult) {
    localStorage.setItem('access_token', x.accessToken);
    localStorage.setItem('refresh_token', x.refreshToken);
    localStorage.setItem('login-event', 'login' + Math.random());
  }

}
