import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Account } from 'src/app/modules/authentication/models/account';
import { AccountService } from 'src/app/modules/authentication/services/account.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  currentUserName: string;

  form = new FormGroup({
    search: new FormControl('', [Validators.required])
  });

  constructor(
    private router: Router,
    private accountService: AccountService) { }

  ngOnInit(): void {
    this.accountService.account$.subscribe(
      (res: Account) => {
        this.currentUserName = res.firstName;
      }
    );
  }

  authorize() {
    this.router.navigate(['account/login']);
  }

  register() {
    this.router.navigate(['account/register']);
  }

  about() {
    this.router.navigate(['about']);
  }

  logout() {
    this.accountService.logout();
    this.currentUserName = '';
  }

  myStores() {
    this.router.navigate(['store/my']);
  }
}
