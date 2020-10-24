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
    Swiper.use([Autoplay]);
    const brandsSwiper = new Swiper ('.brands-swiper', {
      direction: 'horizontal',
      slidesPerView: 7,
      centeredSlides: true,
      loop: true,
      autoplay: {
        disableOnInteraction: false,
        delay: 3000,
      }
    })

  }

}
