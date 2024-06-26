import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ProductModel } from '../../models/product.model';
import { Apollo, gql } from 'apollo-angular';
import { SafeUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css']
})
export class ProductCardComponent implements OnInit {
  @Input() product: ProductModel;
  @Input() mainPhotoUrl: SafeUrl;

  @Output() clicked = new EventEmitter();

  id: number;
  productCategories;

  isLiked: boolean = false;

  isOwner: boolean = false;
  isNotPreview: boolean = false;

  showSecondPhoto: boolean = false;

  productCategoriesQuery = gql`
    query GetProductCategories {
      productCategories {
        id
        name
        mainCategoryId
        subcategories {
          id
          name
          mainCategoryId
        }
      }
    }`;

  constructor(private apollo: Apollo) { }

  ngOnInit() {
    this.apollo
      .query({ query: this.productCategoriesQuery })
      .subscribe((res: any) => {
        this.productCategories = res.data.productCategories;
      });
  }

  togglePhoto(show: boolean) {
    if (this.product?.productPhotos?.length > 1 || this.product?.downloadedPhotos?.length > 1) {
      this.showSecondPhoto = show;
    }
  }

  likeProduct() {
    this.isLiked = !this.isLiked;
  }
}
