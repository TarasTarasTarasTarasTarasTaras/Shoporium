import { Component, Input, OnInit, OnDestroy, ElementRef } from '@angular/core';

@Component({
  selector: 'app-photo-slider',
  templateUrl: './photo-slider.component.html',
  styleUrls: ['./photo-slider.component.css']
})
export class PhotoSliderComponent implements OnInit, OnDestroy {
  @Input() photos: Uint8Array[] = [];

  currentIndex = 0;
  slideWidth: number;

  constructor(private elementRef: ElementRef) {}

  ngOnInit(): void {
    this.setSlideWidth();
    window.addEventListener('resize', this.setSlideWidth.bind(this));
  }

  ngOnDestroy() {
    window.removeEventListener('resize', this.setSlideWidth.bind(this));
  }

  setSlideWidth() {
    const slideElement = this.elementRef.nativeElement.querySelector('.slides');
    this.slideWidth = slideElement.clientWidth;
  }

  nextSlide() {
    this.currentIndex = (this.currentIndex + 1) % this.photos.length;
  }

  prevSlide() {
    this.currentIndex = (this.currentIndex - 1 + this.photos.length) % this.photos.length;
  }
}
