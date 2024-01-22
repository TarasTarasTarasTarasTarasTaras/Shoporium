import { Component, OnInit } from '@angular/core';
import { EMPTY, Subscription, catchError } from 'rxjs';
import { AccountService } from '../../services/account.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Account } from '../../models/account';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  form = new FormGroup({
    firstName: new FormControl('', Validators.required),
    lastName: new FormControl('', Validators.required),
    mobileNumber: new FormControl('', Validators.required),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', Validators.required)
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

  register() {
    let model = {
      firstName: this.f['firstName'].value,
      lastName: this.f['lastName'].value,
      mobileNumber: this.f['mobileNumber'].value,
      email: this.f['email'].value,
      password: this.f['password'].value
    };

    this.accountService.register(model)
      .subscribe(() => {
          this.redirectAfterLogin();
        }),
        catchError(error => {
          return EMPTY;
        });
  }
}
