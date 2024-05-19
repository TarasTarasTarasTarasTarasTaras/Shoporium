import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductCategoriesWindowComponent } from './components/product-categories-window/product-categories-window.component';
import { MainComponent } from './components/main/main.component';

const routes: Routes = [
  { path: '', component: MainComponent },
  { path: 'categories', component: ProductCategoriesWindowComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }