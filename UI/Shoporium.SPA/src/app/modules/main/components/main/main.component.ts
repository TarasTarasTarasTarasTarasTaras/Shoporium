import { Component, OnInit, ViewChildren, QueryList } from '@angular/core';
import { Router } from '@angular/router';
import { StaticDataService } from 'src/app/modules/core/services/static-data.service';
import { ProductModel } from 'src/app/modules/products/models/product.model';
import { ProductService } from 'src/app/modules/products/services/products.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {
  slideConfig = {
    "slidesToShow": 1,
    "slidesToScroll": 1,
    "autoplay": true,
    "autoplaySpeed": 3500,
    "dots": true,
    "arrows": true
  };

  slideProductsConfig = {
    "slidesToShow": 4,
    "slidesToScroll": 1,
    "autoplay": true,
    "autoplaySpeed": 3500,
    "dots": false,
    "arrows": true
  };

  @ViewChildren('slickImage') slickImages: QueryList<any>;
  
  newestProducts: ProductModel[] = [];
  theMostPopularProducts: ProductModel[] = [];
  allProducts: ProductModel[] = [];

  constructor(
    private router: Router,
    private staticDataService: StaticDataService,
    private productService: ProductService) { }
  
  ngOnInit() {
    this.getAllProducts();
    this.getNewestProducts();
    this.getTheMostPopularProducts();
  }

  slides = [
    'assets/images/main/banners/banner_1.png',
    'assets/images/main/banners/banner_2.png',
    'assets/images/main/banners/banner_3.png'
  ];

  getAllProducts() {
    this.productService.getAllProducts().subscribe(products => {
      this.allProducts = products;
    })
  }

  getNewestProducts() {
    this.staticDataService.getNewestProducts().subscribe(products => {
      this.newestProducts = products;
    })
  }

  getTheMostPopularProducts() {
    this.staticDataService.getTheMostPopularProducts().subscribe(products => {
      this.theMostPopularProducts = products;
    })
  }

  openProduct(product: ProductModel) {
    this.router.navigate([`product/details/${product.id}`]);
  }
}
