import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router'
import { __values } from 'tslib';
@Component({
  selector: 'app-tools-page',
  templateUrl: './tools-page.component.html',
  styleUrls: ['./tools-page.component.scss']
})
export class ToolsPageComponent implements OnInit {

  constructor(private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    var requestURL = '../../../assets/tools/electro.json';
    
    const container = document.querySelector('.container');
    this.route.queryParams.subscribe(params => {
      // console.log(params.type);
      container.innerHTML = String(params.type);
    });
  }

}
