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

  tools: MainTools = new MainTools();
  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.tools = data["res"];
      console.log(data["res"]);
    })
  }

}
