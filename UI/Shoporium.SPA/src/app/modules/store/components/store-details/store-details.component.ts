import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { StoreService } from '../../services/store.service';
import { Store } from '../../models/store';
import { Apollo, gql } from 'apollo-angular';
import { AccountService } from 'src/app/modules/authentication/services/account.service';

@Component({
  selector: 'app-store-details',
  templateUrl: './store-details.component.html',
  styleUrls: ['./store-details.component.css']
})
export class StoreDetailsComponent implements OnInit {
  @Input() store: Store;

  id: number;
  storeCategories;

  isOwner: boolean = false;
  isNotPreview: boolean = false;

  productCategoriesQuery = gql`
    query GetStoreCategories {
      storeCategories {
        id
        name
      }
    }`;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private apollo: Apollo,
    private storeService: StoreService,
    private accountService: AccountService ) { }

  ngOnInit() {
    this.apollo
      .query({ query: this.productCategoriesQuery })
      .subscribe((res: any) => {
        this.storeCategories = res.data.storeCategories;
      });

    if (!this.store) {
      this.isNotPreview = true;
      this.id = this.route.snapshot.params["id"];

      this.storeService.getStoreDetails(this.id).subscribe(store => {
        this.store = store;

        this.accountService.account$.subscribe(user => {
          this.isOwner = store.userId == user.userId;
        })
      })
    }
  }

  get category(): string {
    return this.storeCategories.find(c => c.id == this.store.categoryId)?.name;
  }

  get isPhotoSelected(): boolean {
    return this.store.mainPhoto || this.store.downloadedMainPhoto || this.store.backgroundPhoto || this.store.downloadedBackgroundPhoto;
  }

  addProduct() {
    this.router.navigate(['products/add']);
  }
}
