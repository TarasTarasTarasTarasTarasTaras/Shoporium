import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { MaterialModule } from 'src/app/shared/material.module';
import { AddProductComponent } from './components/add-product/add-product.component';
import { ListOfProductsComponent } from './components/list-of-products/list-of-products.component';
import { ProductCardComponent } from './components/product-card/product-card.component';
import { ProductRoutingModule } from './product-routing.module';
import { ListOfProductCardsComponent } from './components/list-of-product-cards/list-of-product-cards.component';
import { ProductDetailComponent } from './components/product-detail/product-detail.component';
import { CoreModule } from '../core/core.module';

@NgModule({
  declarations: [
    ListOfProductsComponent,
    ListOfProductCardsComponent,
    ProductCardComponent,
    AddProductComponent,
    ProductDetailComponent
  ],
  imports: [
    CommonModule,
    ProductRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    MaterialModule,
    CoreModule
  ],
  exports: [
    ListOfProductCardsComponent,
    ProductCardComponent
  ],
  providers: [
  ],
  schemas: [ CUSTOM_ELEMENTS_SCHEMA ]
})
export class ProductModule { }
