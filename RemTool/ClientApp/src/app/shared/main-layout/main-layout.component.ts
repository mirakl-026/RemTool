import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-main-layout',
  templateUrl: './main-layout.component.html',
  styleUrls: ['./main-layout.component.scss']
})
export class MainLayoutComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {

    // widthSet();
    // window.onresize = widthSet;

    function widthSet() {
      const sideDropMenu = document.querySelector('.side-drop-menu');
      const headerNav = document.querySelector('.header__nav');
      let sideDropMenuLPos = Math.round(sideDropMenu.getBoundingClientRect().left);
      let headerNavRPos = Math.round(headerNav.getBoundingClientRect().right);
      let width = 
      sideDropMenu.setAttribute('style', 'width:' + `${headerNavRPos - sideDropMenuLPos}` + 'px;');
    }

  }
}
