import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { StoreService } from '../../services/store.service';
import { Store } from '../../models/store';
import { Apollo, gql } from 'apollo-angular';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-store',
  templateUrl: './create-store.component.html',
  styleUrls: ['./create-store.component.css']
})
export class CreateStoreComponent implements OnInit {
  selectedMainPhoto: any;
  selectedBackgroundPhoto: any;

  storeCategories;

  productCategoriesQuery = gql`
    query GetStoreCategories {
      storeCategories {
        id
        name
      }
    }`;

  form = new FormGroup({
    name: new FormControl('', [Validators.required]),
    description: new FormControl('', Validators.required),
    categoryId: new FormControl(0, Validators.required),
    otherCategoryName: new FormControl(''),
    mainPhoto: new FormControl(),
    backgroundPhoto: new FormControl()
  });

  constructor(
    private router: Router,
    private apollo: Apollo,
    private storeService: StoreService) { }

  ngOnInit() {
    this.apollo
      .query({ query: this.productCategoriesQuery })
      .subscribe((res: any) => {
        this.storeCategories = res.data.storeCategories;
      });
  }

  onFileSelected(event: any, controlName: string) {
    const file = event.target.files[0];
    this.form.get(controlName)?.setValue(file);

    if (!this.form.dirty) this.form.markAsDirty();

    const reader = new FileReader();
    reader.onload = () => {
      if (controlName === 'mainPhoto') {
        this.selectedMainPhoto = reader.result;
      } else if (controlName === 'backgroundPhoto') {
        this.selectedBackgroundPhoto = reader.result;
      }
    };
    reader.readAsDataURL(file);
  }

  submit() {
    const formData = new FormData();
    formData.append('name', this.form.value.name);
    formData.append('description', this.form.value.description);
    formData.append('mainPhoto', this.form.value.mainPhoto);
    formData.append('backgroundPhoto', this.form.value.backgroundPhoto);
    formData.append('categoryId', this.form.value.categoryId.toString());
    if (this.form.value.otherCategoryName) formData.append('otherCategoryName', this.form.value.otherCategoryName);

    this.storeService.createStore(formData).subscribe(storeId => {
      this.router.navigate([`store/details/${storeId}`]);
    })
  }

  get otherCategoryId(): number {
    return this.storeCategories?.find(s => s.name == 'Інше').id;
  }

  onCategoryChange(): void {
    const categoryId = this.form.get('categoryId').value;

    if (categoryId === this.otherCategoryId)
      this.form.get('otherCategoryName').setValidators(Validators.required);
    else
      this.form.get('otherCategoryName').clearValidators();

    this.form.get('otherCategoryName').updateValueAndValidity();
  }

  get store(): Store {
    let store: Store = new Store(
      0,
      this.form.get('name')?.value,
      this.form.get('description')?.value,
      this.selectedMainPhoto,
      this.selectedBackgroundPhoto,
      this.form.get('categoryId').value,
      this.form.get('otherCategoryName').value
    );

    return store;
  }
}
