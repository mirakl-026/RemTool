import { Component, OnInit } from '@angular/core';

import Swiper, { Navigation, Pagination, Autoplay } from 'swiper';
import 'swiper/swiper-bundle.css';
import { RequestServiceService } from '../request-service.service';
import { SendRequestComponent } from '../send-request/send-request.component';

@Component({
  selector: 'app-swiper',
  templateUrl: './swiper.component.html',
  styleUrls: ['./swiper.component.scss'],
})
export class SwiperComponent implements OnInit {
  // reqObj: RequestServiceService = new RequestServiceService();
  
  constructor(
    public reqService: RequestServiceService
  ) { }

  
  ngOnInit(): void {
    function sendRequest(obj){
      // let reqService: RequestServiceService;
      // console.log('swiper send req');
      obj.callMethodOfSecondComponent();
    }

    var reqObj = this.reqService;
    // this.reqService.callMethodOfSecondComponent

    Swiper.use([Navigation, Pagination, Autoplay]);
    const swiper = new Swiper('.swiper-container', {
      // Optional parameters
      direction: 'horizontal',
      loop: true,
      on: {
        click: function(){
          // console.log(this.$el);
        },
        init: function(){
          let swiperReqBtns = document.querySelectorAll('.swiper-request');
          for (let i = 0; i < swiperReqBtns.length; i++) {
            swiperReqBtns[i].addEventListener('click', function(){
              sendRequest(reqObj);
            });
            // console.log(swiperReqBtns[i]);
          }
        }
      },
    
      // If we need pagination
      pagination: {
        el: '.swiper-pagination',
      },
    
      // Navigation arrows
      navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev',
      },
    
      autoplay: {
        delay: 3000,
        disableOnInteraction: false
      },
    })
  }

  // sendRequest(e) {
  //   console.log('swiper send req');
  //   this.reqService.callMethodOfSecondComponent(e);
  // }

}
