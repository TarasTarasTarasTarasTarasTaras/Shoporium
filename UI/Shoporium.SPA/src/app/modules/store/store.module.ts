import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { MaterialModule } from 'src/app/shared/material.module';
import { StoreRoutingModule } from './store-routing.module';
import { AllStoresComponent } from './components/all-stores/all-stores.component';
import { CreateStoreComponent } from './components/create-store/create-store.component';

@NgModule({
  declarations: [
    AllStoresComponent,
    CreateStoreComponent
  ],
  imports: [
    CommonModule,
    StoreRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    MaterialModule
  ],
  providers: [
  ],
  schemas: [ CUSTOM_ELEMENTS_SCHEMA ]
})
export class StoreModule { }
