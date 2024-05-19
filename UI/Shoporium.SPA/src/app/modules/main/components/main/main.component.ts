import { Component, OnInit, ViewChildren, QueryList } from '@angular/core';

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

  @ViewChildren('slickImage') slickImages: QueryList<any>;
  
  constructor() { }

  ngOnInit() {
  }

  slides = [
    'assets/images/main/banners/banner_1.png',
    'assets/images/main/banners/banner_2.png',
    'assets/images/main/banners/banner_3.png'
  ];  
}
