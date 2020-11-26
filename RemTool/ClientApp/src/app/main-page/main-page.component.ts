import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';


@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss']

  
})
export class MainPageComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private router: Router
  ) { }
  fragment;
  ngOnInit(): void {
    // this.route.fragment.subscribe(fragment => {
    //   this.fragment = fragment;
    //   console.log(this.fragment);
    // });
  }

  ngAfterViewChecked(): void {
    try {
        if(this.fragment) {
          console.log(this.fragment);
          document.querySelector('#' + this.fragment).scrollIntoView();
        }
    } catch (e) { }
  }
}