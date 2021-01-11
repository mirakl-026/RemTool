import { Injectable } from '@angular/core';
import { TopPreloaderComponent } from '../top-preloader/top-preloader.component';

export interface ILoader {
  isLoading:boolean;
}

@Injectable({
  providedIn: 'root'
})
export class PreloaderService {
  public preloader: TopPreloaderComponent;


  loader:ILoader={isLoading:false}; 
  showLoader(){
    console.log('showloader started');
    // this.loader.isLoading=true;
    // this.preloader.showPreloader();
  }

  hideLoader(){
    this.loader.isLoading=false;
  }

  isLoading(){
    return this.loader.isLoading;
  }
}
