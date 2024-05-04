import { Component, Input, OnInit } from '@angular/core';
import { ProductModel } from '../../models/product.model';
import { Apollo, gql } from 'apollo-angular';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../../services/products.service';
import { SafeUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css']
})
export class ProductCardComponent implements OnInit {
  @Input() product: ProductModel;
  @Input() mainPhotoUrl: SafeUrl;

  id: number;
  productCategories;

  isOwner: boolean = false;
  isNotPreview: boolean = false;

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

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private apollo: Apollo,
    private productService: ProductService) { }

  ngOnInit() {
    this.apollo
      .query({ query: this.productCategoriesQuery })
      .subscribe((res: any) => {
        this.productCategories = res.data.productCategories;
      });

    if (!this.product) {
      this.isNotPreview = true;
      this.id = this.route.snapshot.params["id"];

      this.productService.getProductDetails(this.id).subscribe(product => {
        this.product = product;
      })
    }
  }
}
