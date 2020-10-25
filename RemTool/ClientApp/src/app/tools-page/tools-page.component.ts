import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router'

@Component({
  selector: 'app-tools-page',
  templateUrl: './tools-page.component.html',
  styleUrls: ['./tools-page.component.scss']
})
export class ToolsPageComponent implements OnInit {

  constructor(private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    // var requestURL = 'https://github.com/nnovofastovskiy/remtool/blob/master/src/assets/tools/electro.json';
    // var request = new XMLHttpRequest();
    // request.open('GET', requestURL);
    // request.responseType = 'json';
    // request.send();
    // request.onload = function() {
    //   var tools = request.response;
    //   console.log(tools);
    // }
    // console.log(toolsJson);
    const container = document.querySelector('.container');
    this.route.queryParams.subscribe(params => {
      // console.log(params.type);
      container.innerHTML = String(params.type);
    });
  }

}
