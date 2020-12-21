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
  pricelist;

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.res$ = data["res"];
    });
  }
}
