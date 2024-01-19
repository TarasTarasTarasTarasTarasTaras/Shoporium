import { Component, OnInit } from '@angular/core';
import { AccountService } from './modules/authentication/services/account.service';
import { Account } from './modules/authentication/models/account';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Shoporium.SPA';

  currentUserName: string;

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
    this.accountService.account$.subscribe(
      (res: Account) => {
        this.currentUserName = res.firstName;
      }
    );
  }
}
