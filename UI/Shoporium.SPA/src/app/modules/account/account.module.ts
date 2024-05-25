import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { MaterialModule } from 'src/app/shared/material.module';
import { ProductModule } from '../products/product.module';
import { AccountRoutingModule } from './account-routing.module';
import { MyProfileComponent } from './components/my-profile/my-profile.component';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import { AccountSidebarComponent } from './components/account-sidebar/account-sidebar.component';
import { MyProfileDataComponent } from './components/my-profile-data/my-profile-data.component';
import { MyAddressDataComponent } from './components/my-address-data/my-address-data.component';

@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    AccountSidebarComponent,
    MyProfileComponent,
    MyProfileDataComponent,
    MyAddressDataComponent
  ],
  imports: [
    CommonModule,
    AccountRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    MaterialModule,
    ProductModule
  ],
  providers: [
  ],
  schemas: [ CUSTOM_ELEMENTS_SCHEMA ]
})
export class AccountModule { }
