import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {
  @Input() category;

  activeCategory = null;

  constructor() { }

  ngOnInit() {
  }

  onMouseEnter(category) {
    this.activeCategory = category;
  }

  onMouseLeave() {
    this.activeCategory = null;
  }

  changeColor(hovered: boolean) {
    const hoveredCategoryId = this.activeCategory.id;

    let subsubcategoryId = this.category.id;
    let subcategoryId = this.category.mainCategoryId;

    if (hovered) {
      if (subsubcategoryId) {
        var element = document.getElementById(`categoryId-${subsubcategoryId}`);
        element.style.color = "red";
      }
  
      if (subcategoryId) {
        var element = document.getElementById(`categoryId-${subcategoryId}`);
        element.style.color = "red";
      }
    }
    else {
      if (subsubcategoryId) {
        var element = document.getElementById(`categoryId-${subsubcategoryId}`);
        element.style.color = "black";
      }
  
      if (subcategoryId) {
        var element = document.getElementById(`categoryId-${subcategoryId}`);
        element.style.color = "black";
      }
    }
  }
}
