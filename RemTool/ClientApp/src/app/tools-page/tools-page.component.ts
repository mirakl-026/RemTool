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
  numOfTools;
  ngOnInit(): void {
  //   // const container = document.querySelector('.container');
  //   this.route.queryParams.subscribe(params => {
  //     this.type = String(params.type);
  //     // container.innerHTML = String(params.type);
  //     if (this.type == "electro") {
  //       this.dataService.getElectroTools().subscribe((data) => {
  //         this.res$ = data;
  //         this.toolNames$ = data["includedTypes"];
  //         this.toolIds$ = data["includedIds"];
  //         this.toolImages$ = data["includedImages"];
  //       });
  //     } else if (this.type == "benzo") {
  //       this.dataService.getFuelTools().subscribe((data) => {
  //         this.res$ = data;
  //         this.toolNames$ = data["includedTypes"];
  //         this.toolIds$ = data["includedIds"];
  //         this.toolImages$ = data["includedImages"];
  //       });
  //     }
  //   });
  // }
    // this.route.queryParams.subscribe(params => {
    //   this.type = String(params.type);
    // });
    this.route.data.subscribe(data => {
      console.log(data["res"]);
      this.res$ = data["res"];
      this.toolNames$ = this.res$["includedTypes"];
      this.toolIds$ = this.res$["includedIds"];
      this.toolImages$ = this.res$["includedImages"];
      // this.numOfTools = this.toolNames$.length;
      this.numOfTools = new Array(this.toolNames$.length)
      // console.log(this.numOfTools)
    })
  }
}
