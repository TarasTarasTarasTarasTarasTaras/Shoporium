import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { StoreService } from '../../services/store.service';
import { Store } from '../../models/store';

@Component({
  selector: 'app-create-store',
  templateUrl: './create-store.component.html',
  styleUrls: ['./create-store.component.css']
})
export class CreateStoreComponent implements OnInit {
  form = new FormGroup({
    name: new FormControl('', [Validators.required]),
    description: new FormControl('', Validators.required)
  });

  constructor(private storeService: StoreService) { }

  ngOnInit() {
  }

  submit() {
    const model: Store = {
      name: this.form.controls['name'].value,
      description: this.form.controls['description'].value,
      mainPhoto: '',
      backgroundPhoto: '',
      otherCategoryName: '',
    }

    this.storeService.createStore(model).subscribe(res => {
      console.log(res)
    })
  }
}
