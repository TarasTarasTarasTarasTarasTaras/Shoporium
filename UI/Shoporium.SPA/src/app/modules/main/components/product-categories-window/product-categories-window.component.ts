import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Apollo, gql } from 'apollo-angular';

@Component({
  selector: 'app-product-categories-window',
  templateUrl: './product-categories-window.component.html',
  styleUrls: ['./product-categories-window.component.css']
})
export class ProductCategoriesWindowComponent implements OnInit {
  productCategories;

  productCategoriesQuery = gql`
    query GetProductCategories {
      productCategories(where: { iconName: { neq: "" } }) {
        name
        iconName
      }
    }`;

constructor(private apollo: Apollo, private http: HttpClient) {
  this.apollo
    .query({ query: this.productCategoriesQuery })
    .subscribe((res: any) => {
      this.productCategories = res.data.productCategories;
    });
}

  ngOnInit() {
  }

}
