import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ProductCategoriesWindowComponent } from './components/product-categories-window/product-categories-window.component';
import { MainRoutingModule } from './main-routing.module';
import { MaterialModule } from 'src/app/shared/material.module';
import { SidebarCategoriesComponent } from './components/sidebar-categories/sidebar-categories.component';
import { MainComponent } from './components/main/main.component';
import { CategoryComponent } from './components/sidebar-categories/components/category/category.component';
import { SlickCarouselModule } from 'ngx-slick-carousel';
import { ProductModule } from '../products/product.module';
import { SearchByComponent } from './components/search-by/search-by.component';

@NgModule({
  declarations: [
    MainComponent,
    ProductCategoriesWindowComponent,
    SidebarCategoriesComponent,
    CategoryComponent,
    SearchByComponent
  ],
  imports: [
    CommonModule,
    MainRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    MaterialModule,
    SlickCarouselModule,
    ProductModule
  ],
  exports: [
    ProductCategoriesWindowComponent
  ],
  providers: [
  ],
  schemas: [ CUSTOM_ELEMENTS_SCHEMA ]
})
export class MainModule { }
