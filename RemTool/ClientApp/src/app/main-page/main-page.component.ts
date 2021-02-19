import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss'] 
})
export class MainPageComponent implements OnInit {

  constructor( ) { }
  ngOnInit(): void {
    document.title = 'Ремтул - ремонт строительного инструмента';
    let heroHeader = document.querySelector('.hero__header');
    heroHeaderResize();
    window.addEventListener('resize', () => {
      heroHeaderResize();
    });
    function heroHeaderResize () {
      let heroHeaderWidth = heroHeader.getBoundingClientRect().width;
      if (window.innerWidth > 991.98){
        heroHeader.setAttribute("style",
        `font-size: ${Math.round(heroHeaderWidth * 0.116)}px;
        line-height: ${Math.round(heroHeaderWidth * 0.16)}px;`)
      } else {
        heroHeader.removeAttribute("style");
        // heroHeader.setAttribute("style",
        // `font-size: ${Math.round(heroHeaderWidth * 0.06)}px;
        // line-height: ${Math.round(heroHeaderWidth * 0.08)}px;`)
      }
    }
  }
}
