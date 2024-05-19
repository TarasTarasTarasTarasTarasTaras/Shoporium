import { Component, OnInit } from '@angular/core';
import { Apollo, gql } from 'apollo-angular';

@Component({
  selector: 'app-sidebar-categories',
  templateUrl: './sidebar-categories.component.html',
  styleUrls: ['./sidebar-categories.component.css']
})
export class SidebarCategoriesComponent implements OnInit {
  productCategories;
  mainCategories;

  expandedCategory: any = null;

  productCategoriesQuery = gql`
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
    }`;

  constructor(private apollo: Apollo) { }

  ngOnInit() {
    this.apollo
    .query({ query: this.productCategoriesQuery })
    .subscribe((res: any) => {
      this.productCategories = res.data.productCategories;
      this.mainCategories = res.data.productCategories.filter(c => c.iconName);
    });
  }

  isCategoryHasSubcategories(category: any): boolean {
    return category.subcategories && category.subcategories.length > 0;
  }

  toggleSubcategories(category: any): void {
    if (this.expandedCategory === category) {
      this.expandedCategory = null;
    } else {
      this.expandedCategory = category;
    }
  }

  activeCategory = null;

  onMouseOver(category) {
    this.activeCategory = category;
  }

  onMouseOut() {
   this.activeCategory = null; 
  }
}
