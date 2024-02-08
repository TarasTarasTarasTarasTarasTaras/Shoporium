import { Component, OnInit } from '@angular/core';
import { Store } from '../../models/store';
import { StoreService } from '../../services/store.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-my-stores',
  templateUrl: './my-stores.component.html',
  styleUrls: ['./my-stores.component.css']
})
export class MyStoresComponent implements OnInit {
  myStores: Store[];

  constructor(
    private router: Router,
    private storeService: StoreService
  ) { }

  ngOnInit(): void {
    this.storeService.getMyStores().subscribe(stores => {
      this.myStores = stores;
    });
  }

  createNewStore() {
    this.router.navigate(['store/create']);
  }

  storeDetails(id: number) {
    this.router.navigate([`store/details/${id}`]);
  }
}
