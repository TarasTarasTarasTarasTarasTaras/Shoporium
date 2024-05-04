import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../../services/products.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Apollo, gql } from 'apollo-angular';
import { ProductModel } from '../../models/product.model';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent implements OnInit {
  storeId;

  productCategories: any[] = [];

  selectedFirstCategory: any = null;
  selectedSecondCategory: any = null;

  subcategories: any[] = [];
  subsubcategories: any[] = [];

  mainProductCategories;
  mainPhotoUrl: SafeUrl;

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

  form = new FormGroup({
    name: new FormControl('', [Validators.required]),
    description: new FormControl('', Validators.required),
    price: new FormControl(0, Validators.required),
    productPhotos: new FormControl([]),
    categoryId: new FormControl(0),
    secondCategoryId: new FormControl(0),
    thirdCategoryId: new FormControl(0),
    status: new FormControl(0, Validators.required),
    cityId: new FormControl(0)
  });

  constructor(
    private apollo: Apollo,
    private router: Router,
    private sanitizer: DomSanitizer,
    private route: ActivatedRoute,
    private productService: ProductService) { }

  ngOnInit() {
    this.apollo
      .query({ query: this.productCategoriesQuery })
      .subscribe((res: any) => {
        this.productCategories = res.data.productCategories;
        this.mainProductCategories = this.productCategories.filter(c => !c.mainCategoryId)
      });

    console.log(this.route.snapshot.paramMap.get('storeId'))
    this.storeId = this.route.snapshot.paramMap.get('storeId');
  }

  onFileSelected(event: any, type: string) {
    if (event.target.files.length > 0) {
      const files = event.target.files;
      let selectedPhotos = [];

      for (let i = 0; i < Math.min(files.length, 8); i++) {
        selectedPhotos.push(files[i]); 
      }

      this.form.get(type).setValue(selectedPhotos);

      const file: File = this.form.get('productPhotos').value[0];

      const reader = new FileReader();
      reader.onload = () => {
        this.mainPhotoUrl = this.sanitizer.bypassSecurityTrustUrl(reader.result as string);
      };
      reader.readAsDataURL(file);
    }
  }

  submit() {
    const selectedCategory = this.productCategories.find(c => c.id == this.form.controls['categoryId'].value);
    if (selectedCategory.subcategories?.length) {
      return;
    }

    const photos: File[] = this.form.value.productPhotos;

    const formData = new FormData();
    formData.append('name', this.form.value.name);
    formData.append('description', this.form.value.description);

    photos.forEach((file, index) => {
      formData.append('productPhotos', file, file.name);
    });

    formData.append('categoryId', this.form.value.categoryId.toString());
    formData.append('price', this.form.value.price.toString());
    formData.append('status', this.form.value.status.toString());
    formData.append('cityId', this.form.value.cityId.toString());
    formData.append('storeId', this.storeId.toString());

    this.productService.createProduct(formData).subscribe(() => {
      this.router.navigate([`store/details/${this.storeId}`]);
    })
}

  onCategorySelectionChange(): void {
    const selectedCategoryId = this.form.get('categoryId')!.value;
    const selectedCategory = this.productCategories.find(category => category.id === selectedCategoryId);

    if (selectedCategory && selectedCategory.subcategories && selectedCategory.subcategories.length > 0) {
      this.form.get('categoryId')!.reset();
      this.productCategories = selectedCategory.subcategories;
    }
  }

  onFirstCategoryChange(categoryId: number) {
    this.selectedFirstCategory = categoryId;
    this.subcategories = this.productCategories.filter(c => c.mainCategoryId == categoryId);

    this.subsubcategories = null;
    this.selectedSecondCategory = null;

    this.form.patchValue({categoryId: categoryId});
  }

  onSecondCategoryChange(categoryId: number) {
    this.selectedSecondCategory = categoryId;
    this.subsubcategories = this.productCategories.filter(c => c.mainCategoryId == categoryId);

    this.form.patchValue({categoryId: categoryId});
  }

  onThirdCategoryChange(categoryId: number) {
    this.form.patchValue({categoryId: categoryId});
  }

  get product(): ProductModel {
    let product: ProductModel = new ProductModel(
      0,
      this.form.get('name')?.value,
      this.form.get('description')?.value,
      this.form.get('categoryId').value,
      this.form.get('price').value,
      this.form.get('productPhotos').value
    );

    return product;
  }
}
