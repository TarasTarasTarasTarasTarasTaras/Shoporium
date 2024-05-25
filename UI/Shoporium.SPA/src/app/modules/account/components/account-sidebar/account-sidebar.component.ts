import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-account-sidebar',
  templateUrl: './account-sidebar.component.html',
  styleUrls: ['./account-sidebar.component.css']
})
export class AccountSidebarComponent implements OnInit {
  currentUrl: string = '';

  constructor(private router: Router) { }

  ngOnInit(): void {
    this.currentUrl = this.router.url;

    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe(() => {
      this.currentUrl = this.router.url;
    });
  }

  isActive(url: string): boolean {
    return this.currentUrl.includes(url);
  }
}
