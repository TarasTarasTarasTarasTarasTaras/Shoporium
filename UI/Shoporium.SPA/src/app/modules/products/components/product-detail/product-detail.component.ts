import { Component, Input, OnInit } from '@angular/core';
import { AccountService } from 'src/app/modules/authentication/services/account.service';
import { ProductService } from '../../services/products.service';
import { Apollo, gql } from 'apollo-angular';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductModel } from '../../models/product.model';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css']
})
export class ProductDetailComponent implements OnInit {
  @Input() product: ProductModel;

  id: number;
  productCategories;

  productCategoriesQuery = gql`
    query GetProductCategories {
      productCategories {
        id
        name
      }
    }`;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private apollo: Apollo,
    private productService: ProductService,
    private accountService: AccountService ) { }

  ngOnInit() {
    this.apollo
      .query({ query: this.productCategoriesQuery })
      .subscribe((res: any) => {
        this.productCategories = res.data.productCategories;
      });

    if (!this.product) {
      this.id = this.route.snapshot.params["id"];

      this.productService.getProductDetails(this.id).subscribe(product => {
        this.product = product;
      })
    }
  }

  get category(): string {
    return this.productCategories.find(c => c.id == this.product.categoryId)?.name;
  }

  buy() {
    
  }
}
