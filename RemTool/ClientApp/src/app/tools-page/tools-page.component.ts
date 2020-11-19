import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router'
import { DataService } from '../DataService/data.service';

@Component({
  selector: 'app-tools-page',
  templateUrl: './tools-page.component.html',
  styleUrls: ['./tools-page.component.scss'],
  providers: [DataService]
})

export class ToolsPageComponent implements OnInit {

  constructor(
  private route: ActivatedRoute,
  private dataService: DataService
  ) { }


  type: string;
  res$;
  toolNames$;
  toolIds$;
  toolImages$;
  ngOnInit(): void {
    // const container = document.querySelector('.container');
    this.route.queryParams.subscribe(params => {
      this.type = String(params.type);
      // container.innerHTML = String(params.type);
      if (this.type == "electro") {
        this.dataService.getElectroTools().subscribe((data) => {
          this.res$ = data;
          this.toolNames$ = data["includedTypes"];
          this.toolIds$ = data["includedIds"];
          this.toolImages$ = data["includedImages"];
        });
      } else if (this.type == "benzo") {
        this.dataService.getFuelTools().subscribe((data) => {
          this.res$ = data;
          this.toolNames$ = data["includedTypes"];
          this.toolIds$ = data["includedIds"];
          this.toolImages$ = data["includedImages"];
        });
      }
    });
  }
}
