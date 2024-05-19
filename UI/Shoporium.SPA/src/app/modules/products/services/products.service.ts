import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { ProductModel } from '../models/product.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private readonly apiUrl = `${environment.apiUrl}product`;

  constructor(
    private http: HttpClient,
    private router: Router) {
  }

  getMyProducts() {
    return this.http.get<ProductModel[]>(`${this.apiUrl}/my`);
  }

  getAllProducts() {
    return this.http.get<ProductModel[]>(`${this.apiUrl}/all`);
  }

  getProductDetails(id: number) {
    return this.http.get<ProductModel>(`${this.apiUrl}/details/${id}`);
  }

  getStoreProducts(storeId: number) {
    return this.http.get<ProductModel[]>(`${this.apiUrl}/store/${storeId}`);
  }

  createProduct(model: FormData) {
    return this.http.post(`${this.apiUrl}`, model);
  }
}