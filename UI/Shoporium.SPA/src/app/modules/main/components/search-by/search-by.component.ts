import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { StaticDataService } from 'src/app/modules/core/services/static-data.service';
import { ProductModel } from 'src/app/modules/products/models/product.model';
import { ProductService } from 'src/app/modules/products/services/products.service';

@Component({
  selector: 'app-search-by',
  templateUrl: './search-by.component.html',
  styleUrls: ['./search-by.component.css']
})
export class SearchByComponent implements OnInit {
  slideConfig = {
    "slidesToShow": 1,
    "slidesToScroll": 1,
    "autoplay": true,
    "autoplaySpeed": 3500,
    "dots": true,
    "arrows": true
  };

  slides = [
    'assets/images/main/banners/banner_1.png',
    'assets/images/main/banners/banner_2.png',
    'assets/images/main/banners/banner_3.png'
  ];

  allProducts: ProductModel[] = [];
  queryKey: string = '';
  categoryName: string = '';

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private productService: ProductService,
    private staticDataService: StaticDataService) { }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.queryKey = params['key'];

      if (this.queryKey == 'category') {
        this.categoryName = params['categoryName'];
        console.log(this.categoryName)
        this.staticDataService.getProductCategories().subscribe(res => {
          const category = res.find(c => c.name == this.categoryName);
          console.log(category)
          if (category) {
            this.getProductsByCategoryId(category.id);
          }
        })
      }
    });
  }

  getProductsByCategoryId(categoryId) {
    this.productService.getProductsByCategoryId(categoryId).subscribe(products => {
      this.allProducts = products;
      console.log(products)
    })
  }

  openProduct(product: ProductModel) {
    this.router.navigate([`product/details/${product.id}`]);
  }
}
