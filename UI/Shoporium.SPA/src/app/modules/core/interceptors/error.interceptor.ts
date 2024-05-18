import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AccountService } from '../../account/services/account.service';
import { Router } from '@angular/router';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private accountService: AccountService,
        private router: Router) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(catchError(err => {
            // this.logger.logException(err);

            if (err.status === 401) {
                this.accountService.logout();
            }

            if (err.error instanceof ProgressEvent) {
                // var msg;
                // this.translateService.get('general.server_didnt_respond').subscribe(res => {
                //     msg = res;
                // });
                return throwError(() => 'server_didnt_respond');
            }

            if (err.error?.emailIsNotConfirmed) {
                return throwError(() => err.error);
            }

            const error = (err && err.error && err.error.message) || (err && err.error) || err.statusText;
            return throwError(() => error);
        }))
    }
}