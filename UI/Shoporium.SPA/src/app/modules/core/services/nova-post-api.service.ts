import { Injectable, OnInit } from '@angular/core';
import { Apollo, gql } from 'apollo-angular';
import { from, map, mergeMap, of } from 'rxjs';
import { ProductService } from '../../products/services/products.service';
import { ProductModel } from '../../products/models/product.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class NovaPostApiService {
    private readonly apiUrl = `${environment.apiUrl}novapost`;

    constructor(private http: HttpClient) {}

    getPostOffices(cityName: string) {
        return this.http.get(`${this.apiUrl}/post-offices/${cityName}`).pipe(map(res => {
          return res;
        }))
      }
}