import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ProductCategoriesWindowComponent } from './components/product-categories-window/product-categories-window.component';
import { MainRoutingModule } from './main-routing.module';
import { MaterialModule } from 'src/app/shared/material.module';

@NgModule({
  declarations: [
    ProductCategoriesWindowComponent
  ],
  imports: [
    CommonModule,
    MainRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    MaterialModule
  ],
  exports: [
    ProductCategoriesWindowComponent
  ],
  providers: [
  ],
  schemas: [ CUSTOM_ELEMENTS_SCHEMA ]
})
export class MainModule { }
