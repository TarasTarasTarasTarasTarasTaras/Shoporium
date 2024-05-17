import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './components/header/header.component';
import { MaterialModule } from 'src/app/shared/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AboutComponent } from './components/about/about.component';
import { FooterComponent } from './components/footer/footer.component';
import { PhotoSliderComponent } from './components/photo-slider/photo-slider.component';
import { BuyInOneClickButtonComponent } from './buttons/buy-in-one-click-button/buy-in-one-click-button.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule
  ],
  declarations: [
    HeaderComponent,
    FooterComponent,
    AboutComponent,
    PhotoSliderComponent,
    BuyInOneClickButtonComponent
  ],
  exports: [
    HeaderComponent,
    FooterComponent,
    PhotoSliderComponent,
    BuyInOneClickButtonComponent
  ]
})
export class CoreModule { }
