import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './modules/core/components/about/about.component';
import { SearchByComponent } from './modules/main/components/search-by/search-by.component';

const routes: Routes = [
  { path: 'about', component: AboutComponent },
  { path: 'searchBy', component: SearchByComponent },
  { path: '', loadChildren: () => import('./modules/main/main.module').then(m => m.MainModule) },
  { path: 'account', loadChildren: () => import('./modules/account/account.module').then(m => m.AccountModule) },
  { path: 'store', loadChildren: () => import('./modules/store/store.module').then(m => m.StoreModule) },
  { path: 'product', loadChildren: () => import('./modules/products/product.module').then(m => m.ProductModule) }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
