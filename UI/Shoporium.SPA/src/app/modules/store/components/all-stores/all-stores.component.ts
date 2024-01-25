import { Component, OnInit } from '@angular/core';
import { StoreService } from '../../services/store.service';
import { Store } from '../../models/store';

@Component({
  selector: 'app-all-stores',
  templateUrl: './all-stores.component.html',
  styleUrls: ['./all-stores.component.css']
})
export class AllStoresComponent implements OnInit {
  stores: Store[];

  constructor(private storeService: StoreService) { }

  ngOnInit() {
    this.storeService.getAllStores().subscribe(res => {
      this.stores = res;
    })
  }
}
