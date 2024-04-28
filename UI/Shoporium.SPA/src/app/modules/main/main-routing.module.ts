import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductCategoriesWindowComponent } from './components/product-categories-window/product-categories-window.component';

const routes: Routes = [
  { path: 'categories', component: ProductCategoriesWindowComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }