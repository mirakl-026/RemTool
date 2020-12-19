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
    window.scrollTo({
      top: 0,
      behavior: "smooth"
    });
      this.route.data.subscribe(data => {
      console.log(data["res"]);
      this.res$ = data["res"];
      this.toolNames$ = this.res$["includedTypes"];
      this.toolIds$ = this.res$["includedIds"];
      this.toolImages$ = this.res$["includedImages"];
      this.numOfTools = new Array(this.toolNames$.length)
    })

  }
  ngDoCheck() {
    // let dropMenu = document.querySelector('.drop-menu');
    // dropMenu.setAttribute('style', 'visibility: hidden');
    // dropMenu.removeAttribute('style');

  }

}
