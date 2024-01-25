import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { Store } from '../models/store';

@Injectable({
  providedIn: 'root'
})
export class StoreService {
  private readonly apiUrl = `${environment.apiUrl}store`;

  constructor(
    private http: HttpClient,
    private router: Router) {
  }

  getAllStores() {
    return this.http.get<Store[]>(`${this.apiUrl}/all`);
  }

  createStore(model: Store) {
    return this.http.post(`${this.apiUrl}`, model);
  }
}
