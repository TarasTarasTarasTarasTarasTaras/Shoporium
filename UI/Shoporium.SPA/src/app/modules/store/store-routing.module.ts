import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AllStoresComponent } from './components/all-stores/all-stores.component';
import { CreateStoreComponent } from './components/create-store/create-store.component';
import { MyStoresComponent } from './components/my-stores/my-stores.component';
import { StoreDetailsComponent } from './components/store-details/store-details.component';

const routes: Routes = [
    { path: 'my', component: MyStoresComponent },
    { path: 'all', component: AllStoresComponent },
    { path: 'create', component: CreateStoreComponent },
    { path: 'details/:id', component: StoreDetailsComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StoreRoutingModule { }