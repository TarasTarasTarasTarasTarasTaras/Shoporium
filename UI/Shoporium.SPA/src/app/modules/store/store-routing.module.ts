import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AllStoresComponent } from './components/all-stores/all-stores.component';
import { CreateStoreComponent } from './components/create-store/create-store.component';

const routes: Routes = [
    { path: 'all', component: AllStoresComponent },
    { path: 'create', component: CreateStoreComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StoreRoutingModule { }