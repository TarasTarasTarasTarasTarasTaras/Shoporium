import { Injectable } from '@angular/core';
import { Apollo, gql } from 'apollo-angular';
import { from, map, mergeMap, of } from 'rxjs';
import { ProductService } from '../../products/services/products.service';
import { ProductModel } from '../../products/models/product.model';

@Injectable({
  providedIn: 'root',
})
export class StaticDataService {
  private cities = [];
  private innerCities = [];

  private productCategories = [];
  private mainCategories = [];
  private newestProducts: ProductModel[] = [];
  private theMostPopularProducts: ProductModel[] = [];

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

  private citiesQuery = gql`
    query GetCities {
      cities {
        id
        name
        region
      }
    }
  `;

  private innerCitiesQuery = gql`
    query GetInnerCities {
      innerCities {
        id
        name
        regionId
      }
    }
  `;

  constructor(
    private apollo: Apollo,
    private productService: ProductService) {}

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

  getNewestProducts() {
    if (this.newestProducts?.length > 0) {
      return of(this.newestProducts);
    }

    return this.productService.getNewestProducts().pipe(map(products => {
      this.newestProducts = products;
      return products;
    }));
  }

  getTheMostPopularProducts() {
    if (this.theMostPopularProducts?.length > 0) {
      return of(this.theMostPopularProducts);
    }

    return this.productService.getTheMostPopularProducts().pipe(map(products => {
      this.theMostPopularProducts = products;
      return products;
    }));
  }

  getCities() {
    if (this.cities?.length > 0) {
      return of(this.cities);
    }

    return from(this.apollo.query({ query: this.citiesQuery })).pipe(
      map((res: any) => res.data.cities),
      mergeMap((cities) => {
        this.cities = cities;
        return of(this.cities);
      })
    );
  }

  getInnerCities() {
    if (this.innerCities?.length > 0) {
      return of(this.innerCities);
    }

    return from(this.apollo.query({ query: this.innerCitiesQuery })).pipe(
      map((res: any) => res.data.innerCities),
      mergeMap((innerCities) => {
        this.innerCities = innerCities;
        return of(this.innerCities);
      })
    );
  }
}
