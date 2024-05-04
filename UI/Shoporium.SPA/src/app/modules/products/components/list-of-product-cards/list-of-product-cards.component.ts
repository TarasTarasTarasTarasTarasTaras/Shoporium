import { Component, Input, OnInit } from '@angular/core';
import { ProductModel } from '../../models/product.model';
import { ProductService } from '../../services/products.service';

@Component({
  selector: 'app-list-of-product-cards',
  templateUrl: './list-of-product-cards.component.html',
  styleUrls: ['./list-of-product-cards.component.css']
})
export class ListOfProductCardsComponent implements OnInit {
  @Input() storeId: number;

  products: ProductModel[];

  constructor(
    private productService: ProductService) { }

  ngOnInit() {
    this.productService.getStoreProducts(this.storeId).subscribe(res => {
      this.products = res;
    })
  }
}
