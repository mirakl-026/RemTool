import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-main-layout',
  templateUrl: './main-layout.component.html',
  styleUrls: ['./main-layout.component.scss']
})
export class MainLayoutComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
    let headerDrop = document.querySelector('.header__drop');
    let dropLink = document.querySelector('.drop-link');
    let dropElectro = document.querySelector('.header__drop-electro');
    let dropBenzo = document.querySelector('.header__drop-benzo');
    let headerNav = document.querySelector('.header__nav');
    let dropXpos:number;
    let dropYpos:number;
    let rightXpos:number;
    let rightWidth:number;

    // xpos();
    // window.onload = xpos;
    // window.onresize = xpos;
    
    function xpos() {
      dropXpos = Math.round(dropLink.getBoundingClientRect().left);
      dropYpos = Math.round(dropLink.getBoundingClientRect().bottom);
      
      headerDrop.setAttribute('style', 'left:' + `${dropXpos}` + 'px; top:' + 
      `${dropYpos}` + 'px;');

      rightXpos = Math.round(headerDrop.getBoundingClientRect().width);

      rightWidth = Math.round(headerNav.getBoundingClientRect().right - 
      headerDrop.getBoundingClientRect().right);

      dropElectro.setAttribute('style', 'left:' + `${rightXpos}` + 'px; width: ' + 
      `${rightWidth}` + 'px;');
      dropBenzo.setAttribute('style', 'left:' + `${rightXpos}` + 'px; width: ' + 
      `${rightWidth}` + 'px;');
    }
  }
}
