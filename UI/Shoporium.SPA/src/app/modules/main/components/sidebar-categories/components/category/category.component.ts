import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { StaticDataService } from 'src/app/modules/core/services/static-data.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {
  @Input() category;

  activeCategory = null;

  constructor(
    private router: Router,
    private staticDataService: StaticDataService) { }

  categoryNavigate(category) {
    this.router.navigate(['searchBy'], { queryParams: { key: 'category', categoryName: category.name } });
  }

  ngOnInit() {
  }

  onMouseEnter(category) {
    this.activeCategory = category;
  }

  onMouseLeave() {
    this.activeCategory = null;
  }

  changeColor(hovered: boolean) {
    this.staticDataService.getProductCategories().subscribe(categories => {
      const hoveredCategoryId = this.activeCategory.id;
      const subcategory = categories.find(c => c.id == this.activeCategory.mainCategoryId);
      const mainCategory = categories.find(c => c.id == subcategory?.mainCategoryId);
      
      const color = hovered ? "red" : "black";
      
      const changeElementColor = (id: string | undefined) => {
        if (id) {
          const element = document.getElementById(`categoryId-${id}`);
          if (element) {
            element.style.color = color;
          }
        }
      };
      
      changeElementColor(hoveredCategoryId);
      changeElementColor(subcategory?.id);
      changeElementColor(mainCategory?.id);
    });
  }
}
