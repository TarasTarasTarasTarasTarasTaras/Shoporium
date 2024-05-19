import { Injectable } from '@angular/core';
import { Apollo, gql } from 'apollo-angular';
import { from, map, mergeMap, of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class StaticDataService {
  private productCategories = [];
  private mainCategories = [];

  private productCategoriesQuery = gql`
    query GetProductCategories {
      productCategories {
        id
        name
        iconName
        mainCategoryId
        subcategories {
          id
          name
          mainCategoryId
          subcategories {
            id
            name
            mainCategoryId
          }
        }
      }
    }
  `;

  constructor(private apollo: Apollo) {}

  getProductCategories() {
    if (this.productCategories?.length > 0) {
      return of(this.productCategories);
    }

    return from(this.apollo.query({ query: this.productCategoriesQuery })).pipe(
      map((res: any) => res.data.productCategories),
      mergeMap((categories) => {
        this.productCategories = categories;
        
        let filteredCategories = categories.filter((c) => c.iconName);

        if (filteredCategories.length > 4) {
            filteredCategories = filteredCategories.slice(0, -4);
        }

        this.mainCategories = filteredCategories;
        return of(this.productCategories);
      })
    );
  }

  getMainCategories() {
    if (this.mainCategories?.length > 0) {
      return of(this.mainCategories);
    }

    return from(this.apollo.query({ query: this.productCategoriesQuery })).pipe(
      map((res: any) => res.data.productCategories),
      mergeMap((categories) => {
        this.productCategories = categories;

        let filteredCategories = categories.filter((c) => c.iconName);

        if (filteredCategories.length > 4) {
            filteredCategories = filteredCategories.slice(0, -4);
        }

        this.mainCategories = filteredCategories;
        return of(this.mainCategories);
      })
    );
  }
}