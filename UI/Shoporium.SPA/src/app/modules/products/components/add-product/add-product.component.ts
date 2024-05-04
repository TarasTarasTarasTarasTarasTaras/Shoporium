import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../../services/products.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Apollo, gql } from 'apollo-angular';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent implements OnInit {
  storeId: number;

  productCategories: any[] = [];

  selectedFirstCategory: any = null;
  selectedSecondCategory: any = null;

  subcategories: any[] = [];
  subsubcategories: any[] = [];

  mainProductCategories;

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
          # тут можна додати будь-які інші потрібні властивості
        }
      }
    }`;

  form = new FormGroup({
    name: new FormControl('', [Validators.required]),
    description: new FormControl('', Validators.required),
    price: new FormControl(0, Validators.required),
    productPhotos: new FormControl([]),
    categoryId: new FormControl(0),
    secondCategoryId: new FormControl(0),
    thirdCategoryId: new FormControl(0),
    status: new FormControl(0, Validators.required),
  });

  constructor(
    private apollo: Apollo,
    private router: Router,
    private route: ActivatedRoute,
    private productService: ProductService) { }

  ngOnInit() {
    this.apollo
      .query({ query: this.productCategoriesQuery })
      .subscribe((res: any) => {
        this.productCategories = res.data.productCategories;
        this.mainProductCategories = this.productCategories.filter(c => !c.mainCategoryId)
      });

    this.storeId = this.route.snapshot.params["storeId"];
  }

  onFileSelected(event: any, type: string) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.form.get(type).setValue(file);
    }
  }

  submit() {
  }

  onCategorySelectionChange(): void {
    const selectedCategoryId = this.form.get('categoryId')!.value;
    const selectedCategory = this.productCategories.find(category => category.id === selectedCategoryId);

    if (selectedCategory && selectedCategory.subcategories && selectedCategory.subcategories.length > 0) {
      // Якщо вибрана категорія має підкатегорії, очищаємо поле вибору та встановлюємо підкатегорії
      this.form.get('categoryId')!.reset();
      this.productCategories = selectedCategory.subcategories;
    }
  }

  onFirstCategoryChange(categoryId: number) {
    this.selectedFirstCategory = categoryId;
    this.subcategories = this.productCategories.filter(c => c.mainCategoryId == categoryId);

    this.subsubcategories = null;
    this.selectedSecondCategory = null;
  }

  onSecondCategoryChange(categoryId: number) {
    this.selectedSecondCategory = categoryId;
    this.subsubcategories = this.productCategories.filter(c => c.mainCategoryId == categoryId);
  }
}
