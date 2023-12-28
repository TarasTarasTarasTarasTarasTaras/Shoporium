import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Router } from '@angular/router';
import { BehaviorSubject, EMPTY, Observable, of, Subscription } from 'rxjs';
import { catchError, concatMap, delay, map, tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Account } from '../models/account';
import { Role } from '../models/role';
import { LoginResult } from '../models/login-result';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private readonly apiUrl = `${environment.apiUrl}account`;
  private timer: Subscription;

  private _accountSubject: BehaviorSubject<Account | null> = new BehaviorSubject<Account>(null);
  public account$: Observable<Account> = this._accountSubject.asObservable();

  private storageEventListener(event: StorageEvent) {
    if (event.storageArea === localStorage) {
      if (event.key === 'logout-event') {
        this.stopTokenTimer();
        this._accountSubject.next(null);
      }
      if (event.key === 'login-event') {
        this.stopTokenTimer();
        this.startTokenTimer();
        this.getCurrentUser();
      }
    }
  }


  constructor(private http: HttpClient,
    private router: Router) {
    window.addEventListener('storage', this.storageEventListener.bind(this));
  }

  ngOnDestroy(): void {
    window.removeEventListener('storage', this.storageEventListener.bind(this));
  }

  login(email: string, password: string) {
    return this.http.post<LoginResult>(`${this.apiUrl}/login`, { email, password }, { withCredentials: true }).pipe(
      concatMap((tokens: any) => {
        this.setLocalStorage(tokens);
        this.startTokenTimer();
        return this.getCurrentUser()
      })
    );
  }

  logout(useReturnUrl: boolean = true) {
    this.clearLocalStorage();
    this._accountSubject.next(null);
    // this.dialogRef.closeAll();

    this.stopTokenTimer();

    const loginUrl = '/account/login';
    const returnUrl = this.router.routerState.snapshot.url;

    const returnUrlParam = !useReturnUrl || returnUrl.includes(loginUrl) ? null :
      { returnUrl: returnUrl };
    this.router.navigate([loginUrl], { queryParams: returnUrlParam, });
  }

  refreshToken() {
    const refreshToken = localStorage.getItem('refresh_token');
    if (!refreshToken) {
      return of(null);
    }

    return this.http
      .post<LoginResult>(`${this.apiUrl}/refresh-token`, { refreshToken })
      .pipe(
        concatMap((tokens) => {
          this.setLocalStorage(tokens);
          this.startTokenTimer();
          return this.getCurrentUser()
        }),
        catchError(() => {
          this.logout();
          return EMPTY;
        }));
  }

  validatePassword(passwordHash: string) {
    let params = new HttpParams().set('password', passwordHash);
    return this.http.get(`${this.apiUrl}/validate-password`, { params: params });
  }

  // helper methods
  setLocalStorage(x: LoginResult) {
    if (!x) return;

    localStorage.setItem('access_token', x?.accessToken);
    localStorage.setItem('refresh_token', x?.refreshToken);
    localStorage.setItem('login-event', 'login' + Math.random());
  }

  clearLocalStorage() {
    localStorage.removeItem('access_token');
    localStorage.removeItem('refresh_token');
    localStorage.setItem('logout-event', 'logout' + Math.random());
  }

  getCurrentUser() {
    return this.http.get<LoginResult>(`${this.apiUrl}/user`).pipe(map(x => {
      this._accountSubject.next({
        userId: x.role != Role.HealthCardOwner ? x.id : 0,
        healthCardId: x.role == Role.HealthCardOwner ? x.id : 0,
        firstName: x.firstName,
        lastName: x.lastName,
        email: x.email,
        role: x.role
      });
    }));
  }

  getCurrentUserRole() {
    return this._accountSubject.value?.role;
  }

  private getTokenRemainingTime() {
    const accessToken = localStorage.getItem('access_token');
    if (!accessToken) {
      return 0;
    }
    const jwtToken = JSON.parse(atob(accessToken.split('.')[1]));
    const expires = new Date(jwtToken.exp * 1000);
    return expires.getTime() - Date.now();
  }

  private startTokenTimer() {
    const timeout = this.getTokenRemainingTime();

    this.timer = of(true)
      .pipe(
        delay(timeout),
        tap(() => this.refreshToken().subscribe())
      )
      .subscribe();
  }

  private stopTokenTimer() {
    this.timer?.unsubscribe();
  }
}
