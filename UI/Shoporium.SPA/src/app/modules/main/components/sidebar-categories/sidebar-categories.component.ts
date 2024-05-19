import { Component, OnInit } from '@angular/core';
import { StaticDataService } from 'src/app/modules/core/services/static-data.service';

@Component({
  selector: 'app-sidebar-categories',
  templateUrl: './sidebar-categories.component.html',
  styleUrls: ['./sidebar-categories.component.css']
})
export class SidebarCategoriesComponent implements OnInit {
  mainCategories;
  productCategories;

  expandedCategory: any = null;
  constructor(private staticDataService: StaticDataService) { }

  ngOnInit() {
    this.staticDataService.getMainCategories().subscribe(res => this.mainCategories = res);
    this.staticDataService.getProductCategories().subscribe(res => this.productCategories = res);
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
