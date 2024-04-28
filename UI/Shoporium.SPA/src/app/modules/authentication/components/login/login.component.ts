import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { catchError, EMPTY, Subscription } from 'rxjs';
import { AccountService } from '../../services/account.service';
import { Account } from '../../models/account';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {
  form = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('')//Validators.required)
  });

  private subscription: Subscription;

  loading = false;
  account: Account;
  
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private accountService: AccountService) {

  }

  ngOnInit(): void {
    this.subscription = this.accountService.account$.subscribe((x) => {
      const accessToken = localStorage.getItem('access_token');
      const refreshToken = localStorage.getItem('refresh_token');
      if (x && accessToken && refreshToken) {
        this.redirectAfterLogin();
      }
    });
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }

  get f() { return this.form.controls; }

  private redirectAfterLogin() {
    const returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    this.router.navigate([returnUrl]);
  }

  forgotPasswordClicked() {
    this.router.navigate(['account/forgot-password']);
  }

  login() {
    let email: string = this.f['email'].value;
    let password: string = this.f['password'].value;

    this.accountService.login(email, password)
      .subscribe(() => {
          this.redirectAfterLogin();
        }),
        catchError(error => {
          return EMPTY;
        });
  }

  register() {
    this.router.navigate(['account/register']);
  }
}