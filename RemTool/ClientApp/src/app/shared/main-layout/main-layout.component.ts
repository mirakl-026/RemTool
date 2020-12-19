import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MainTools } from '../resolver.service';

@Component({
  selector: 'app-main-layout',
  templateUrl: './main-layout.component.html',
  styleUrls: ['./main-layout.component.scss']
})
export class MainLayoutComponent implements OnInit {

  constructor(
    private route: ActivatedRoute
  ) { }

  dropMenuFlag: boolean = false;

  sideElectro: boolean = false;
  sideFuel: boolean = false;
  sideGarden: boolean = false;
  sideCompressors: boolean = false;
  sideGenerators: boolean = false;
  sideWelding: boolean = false;
  sideHeatguns: boolean = false;
  sideRest: boolean = false;

  searchMobile: boolean = false;

  tools: MainTools = new MainTools();

  burger: boolean = false;
  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.tools = data["res"];
    });

    window.onresize = () => {
      if (window.matchMedia("(max-width: 683.98px)").matches) {
        this.searchMobile = true;
      } else {
        this.searchMobile = false;
      }
    }

    if (window.matchMedia("(max-width: 683.98px)").matches) {
      this.searchMobile = true;
    } else {
      this.searchMobile = false;
    }
  }

  dropMenuHide() {
    this.dropMenuFlag = false;
    this.sideElectro = false;
    this.sideFuel = false;
    this.sideGarden = false;
    this.sideCompressors = false;
    this.sideGenerators = false;
    this.sideWelding = false;
    this.sideHeatguns = false;
    this.sideRest = false;
  }

  arrowDown(e) {
    if (window.matchMedia("(hover: hover)").matches) {
      this.dropMenuFlag = true;
    } else {
      if (e == 't')
        this.dropMenuFlag = !this.dropMenuFlag;
    }
  }

  arrowRight(type){
    if (window.matchMedia("(hover: hover)").matches) {
      if (type =='electrom') {
        this.sideElectro = true;
      } else if (type == 'benzom') {
        this.sideFuel = true;
      } else if (type == 'gardenm') {
        this.sideGarden = true;
      } else if (type == 'compressorm') {
        this.sideCompressors = true;
      } else if (type == 'generatorm') {
        this.sideGenerators = true;
      } else if (type == 'weldingm') {
        this.sideWelding = true;
      } else if (type == 'heatgunm') {
        this.sideHeatguns = true;
      } else if (type == 'restm') {
        this.sideRest = true;
      }
    } else {
      if (type =='electro') {
        this.sideElectro = !this.sideElectro;
      } else if (type =='benzo') {
        this.sideFuel = !this.sideFuel;
      } else if (type =='garden') {
        this.sideGarden = !this.sideGarden;
      } else if (type =='compressor') {
        this.sideCompressors = !this.sideCompressors;
      } else if (type =='generator') {
        this.sideGenerators = !this.sideGenerators;
      } else if (type =='welding') {
        this.sideWelding = !this.sideWelding;
      } else if (type =='heatgun') {
        this.sideHeatguns = !this.sideHeatguns;
      } else if (type =='rest') {
        this.sideRest = !this.sideRest;
      }
    }
  }

  // clickBurger() {
  //   if (!this.burger) {
  //     // document.getElementsByTagName('body')[0].classList.add('locked');
  //     document.body.classList.add('locked');
  //   } else {
  //     document.body.classList.remove('locked');
  //   }
  // }
}
