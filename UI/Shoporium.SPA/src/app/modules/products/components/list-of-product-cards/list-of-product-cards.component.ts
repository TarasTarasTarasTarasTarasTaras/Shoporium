import { Component, Input, OnInit } from '@angular/core';
import { ProductModel } from '../../models/product.model';
import { ProductService } from '../../services/products.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-list-of-product-cards',
  templateUrl: './list-of-product-cards.component.html',
  styleUrls: ['./list-of-product-cards.component.css']
})
export class ListOfProductCardsComponent implements OnInit {
  @Input() storeId: number;

  @Input() products: ProductModel[] = [];

  constructor(
    private router: Router,
    private productService: ProductService) { }

  ngOnInit() {
    if (!this.products.length) {
      this.productService.getStoreProducts(this.storeId).subscribe(res => {
        this.products = res;
      })
    }
  }

  openProduct(product: ProductModel) {
    this.router.navigate([`product/details/${product.id}`]);
  }
}
