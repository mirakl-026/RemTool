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

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      // console.log(data["tool"]);
      this.res$ = data["tool"];
    });
    // this.route.queryParams.subscribe(params => {
    //   this.id = String(params.id);
    //   this.dataService.getToolType(this.id).subscribe(data => {
    //     console.log(data);
    //     this.res$ = data;
    //   });
      // container.innerHTML = String(params.type);
    // });

  }

}
