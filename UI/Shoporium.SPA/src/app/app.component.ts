import { Component, OnInit } from '@angular/core';
import { AccountService } from './modules/authentication/services/account.service';
import { Account } from './modules/authentication/models/account';
import { Router } from '@angular/router';
import { Apollo, gql } from 'apollo-angular';

const productCategories = gql`query GetProductCategories {
  productCategories {
    name
  }
}`;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Shoporium.SPA';

  currentUserName: string;
  productCategories;

  constructor(
    private apollo: Apollo,
    private router: Router,
    private accountService: AccountService) {
      apollo.query({ query: productCategories }).subscribe(res => {
        this.productCategories = res.data;
        console.log(res)
      })
    }

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

  logout() {
    this.accountService.logout();
    this.currentUserName = '';
  }
}
