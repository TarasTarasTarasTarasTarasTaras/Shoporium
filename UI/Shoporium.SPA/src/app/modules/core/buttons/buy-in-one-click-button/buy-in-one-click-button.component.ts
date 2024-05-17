import { Component, ElementRef, EventEmitter, OnInit, Output, Renderer2, ViewChild } from '@angular/core';
import { gsap } from 'gsap';

@Component({
  selector: 'app-buy-in-one-click-button',
  templateUrl: './buy-in-one-click-button.component.html',
  styleUrls: ['./buy-in-one-click-button.component.css']
})
export class BuyInOneClickButtonComponent implements OnInit {
  @Output() clicked = new EventEmitter<any>();
  @ViewChild('truckButton') button: ElementRef;

  constructor(private renderer: Renderer2) { }

  ngOnInit() {
  }

  handleClick(event: Event): void {
    event.preventDefault();

    const button = this.button.nativeElement;
    this.clicked.emit();

    const box = button.querySelector('.box');
    const truck = button.querySelector('.truck');

    if (!button.classList.contains('done')) {
      if (!button.classList.contains('animation')) {

        this.renderer.addClass(button, 'animation');

        gsap.to(button, {
          '--box-s': 1,
          '--box-o': 1,
          duration: 0.3,
          delay: 0.5
        });

        gsap.to(box, {
          x: 0,
          duration: 0.4,
          delay: 0.7
        });

        gsap.to(button, {
          '--hx': -5,
          '--bx': 50,
          duration: 0.18,
          delay: 0.92
        });

        gsap.to(box, {
          y: 0,
          duration: 0.1,
          delay: 1.15
        });

        gsap.set(button, {
          '--truck-y': 0,
          '--truck-y-n': -26
        });

        gsap.to(button, {
          '--truck-y': 1,
          '--truck-y-n': -25,
          duration: 0.2,
          delay: 1.25,
          onComplete: () => {
            gsap.timeline({
              onComplete: () => {
                this.renderer.addClass(button, 'done');
              }
            })
            .to(truck, {
              x: 0,
              duration: 0.4
            })
            .to(truck, {
              x: 40,
              duration: 1
            })
            .to(truck, {
              x: 20,
              duration: 0.6
            })
            .to(truck, {
              x: 96,
              duration: 0.4
            });
            
            gsap.to(button, {
              '--progress': 1,
              duration: 2.4,
              ease: "power2.in"
            });
          }
        });
      }
    } else {
      this.renderer.removeClass(button, 'animation');
      this.renderer.removeClass(button, 'done');
      
      gsap.set(truck, {
        x: 4
      });

      gsap.set(button, {
        '--progress': 0,
        '--hx': 0,
        '--bx': 0,
        '--box-s': 0.5,
        '--box-o': 0,
        '--truck-y': 0,
        '--truck-y-n': -26
      });

      gsap.set(box, {
        x: -24,
        y: -6
      });
    }
  }
}
