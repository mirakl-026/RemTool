import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DataService } from '../DataService/data.service';

@Component({
  selector: 'app-tool-page',
  templateUrl: './tool-page.component.html',
  styleUrls: ['./tool-page.component.scss'],
  providers: [DataService]
})
export class ToolPageComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private dataService: DataService
  ) { }

  id: string;
  res$;
  pricelist$: string[] = [];

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.res$ = data["res"];
      console.log(this.res$)
      for (let i = 0; i < data["res"].serves.length; i++) {
        this.pricelist$.push(data["res"].serves[i]);
        this.pricelist$.push(data["res"].costs[i]);
      }
      console.log(this.pricelist$);
    });
  }
}
