import { Component, OnInit } from '@angular/core';
// import { PreloaderService } from '../shared/preloader.service';

@Component({
  selector: 'app-top-preloader',
  templateUrl: './top-preloader.component.html',
  styleUrls: ['./top-preloader.component.scss']
})
export class TopPreloaderComponent implements OnInit {

  constructor(
    // private preloaderService: PreloaderService
  ) { }

  preloader: boolean = false;

  ngOnInit(): void {
    // this.preloader = true;
  }

  showPreloader(){
    this.preloader = true;
  }
  hidePreloader(){
    this.preloader = false;
  }

}
