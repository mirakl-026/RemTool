import { Component, OnInit } from '@angular/core';
import { PreloaderService } from '../shared/preloader.service';

@Component({
  selector: 'app-preloader',
  templateUrl: './preloader.component.html',
  styleUrls: ['./preloader.component.scss']
})
export class PreloaderComponent implements OnInit {

  constructor(
    private preloaderService: PreloaderService
  ) { }
  preloader: boolean;
  ngOnInit(): void {
    this.preloader = false;
  }

}
