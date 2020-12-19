import { Component, OnInit } from '@angular/core';
import Swiper, { Autoplay } from 'swiper';
import 'swiper/swiper-bundle.css';

@Component({
  selector: 'app-swiper-brands',
  templateUrl: './swiper-brands.component.html',
  styleUrls: ['./swiper-brands.component.scss']
})
export class SwiperBrandsComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
    window.addEventListener('resize', () => {
      var slidesPerView: number;
      if (window.matchMedia("(max-width: 730.98px)").matches) {
        slidesPerView = 3;
        /* the viewport is at least 400 pixels wide */
      } else if (window.matchMedia("(max-width: 991.98px)").matches) {
        slidesPerView = 5;
        /* the viewport is at least 400 pixels wide */
      } else {
        slidesPerView = 7;
        /* the viewport is less than 400 pixels wide */
      }
      brandsSwiper.params.slidesPerView = slidesPerView;
    })
    var slidesPerView: number;
    if (window.matchMedia("(max-width: 991.98px)").matches) {
      slidesPerView = 5;
      /* the viewport is at least 400 pixels wide */
    } else {
      slidesPerView = 7;
      /* the viewport is less than 400 pixels wide */
    }
    Swiper.use([Autoplay]);
    const brandsSwiper = new Swiper ('.brands-swiper', {
      direction: 'horizontal',
      slidesPerView: slidesPerView,
      centeredSlides: true,
      loop: true,
      autoplay: {
        disableOnInteraction: false,
        delay: 3000,
      }
    })

  }

}
