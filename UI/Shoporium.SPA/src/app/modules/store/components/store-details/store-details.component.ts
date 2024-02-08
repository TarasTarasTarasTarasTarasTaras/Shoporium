import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { StoreService } from '../../services/store.service';
import { Store } from '../../models/store';
import { Apollo, gql } from 'apollo-angular';

@Component({
  selector: 'app-store-details',
  templateUrl: './store-details.component.html',
  styleUrls: ['./store-details.component.css']
})
export class StoreDetailsComponent implements OnInit {
  @Input() store: Store;

  id: number;
  storeCategories;

  productCategoriesQuery = gql`
    query GetStoreCategories {
      storeCategories {
        id
        name
      }
    }`;

  constructor(
    private route: ActivatedRoute,
    private apollo: Apollo,
    private storeService: StoreService) { }

  ngOnInit() {
    this.apollo
      .query({ query: this.productCategoriesQuery })
      .subscribe((res: any) => {
        this.storeCategories = res.data.storeCategories;
      });

    if (!this.store) {
      this.id = this.route.snapshot.params["id"];

      this.storeService.getStoreDetails(this.id).subscribe(store => {
        this.store = store;
      })
    }
  }

  get category(): string {
    return this.storeCategories.find(c => c.id == this.store.categoryId)?.name;
  }
}
