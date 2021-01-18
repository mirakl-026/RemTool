import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ActivatedRouteSnapshot, Router } from '@angular/router'
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { DataService } from '../DataService/data.service';

@Component({
  selector: 'app-tools-page',
  templateUrl: './tools-page.component.html',
  styleUrls: ['./tools-page.component.scss'],
  providers: [DataService]
})

export class ToolsPageComponent implements OnInit {
  private destroy$ = new Subject<undefined>();
  constructor(
    private route: ActivatedRoute,
    private dataService: DataService
  ) {
  }

  preloader: boolean = false;

  category: string;
  type;
  res$;
  toolNames$: string[];
  toolIds$;
  toolImages$;
  numOfTools: number[];
  ngOnInit(): void {
    // this.type = this.route.snapshot.params.type;
    this.route.params.pipe(
      takeUntil(this.destroy$))
      .subscribe(params => {
        // window.scrollTo({
        //   top: 0,
        //   behavior: "smooth"
        // });    
        this.type = params.type;
        console.log(this.type);
        this.preloader = true;
        this.getTools();
      });
  }

  getTools() {
    if (this.type == 'electro') {
      this.dataService.getElectroTools().subscribe(data => {
        this.getRes(data);
        this.category = "Электроинструмент";
      });
    } else if (this.type == 'benzo') {
      this.dataService.getFuelTools().subscribe(data => {
        this.getRes(data)
        this.category = "Бензоинструмент";
      });
    } else if (this.type == "garden") {
      this.dataService.getGardenTools().subscribe(data => {
        this.getRes(data)
        this.category = "Садовая техника";
      });
    } else if (this.type == "compressor") {
      this.dataService.getCompressors().subscribe(data => {
        this.getRes(data)
        this.category = "Компрессоры";
      });
    } else if (this.type == "generator") {
      this.dataService.getGenerators().subscribe(data => {
        this.getRes(data)
        this.category = "Генераторы";
      });
    } else if (this.type == "welding") {
      this.dataService.getWeldingTools().subscribe(data => {
        this.getRes(data)
        this.category = "Сварочные аппараты";
      });
    } else if (this.type == "heatgun") {
      this.dataService.getHeatGuns().subscribe(data => {
        this.getRes(data)
        this.category = "Тепловые пушки";
      });
    } else if (this.type == "rest") {
      this.dataService.getRestTools().subscribe(data => {
        this.getRes(data)
        this.category = "Техника для отдыха";
      });
    }
  }

  getRes(data) {
    this.res$ = data;
    this.toolNames$ = this.res$["includedTypes"];
    this.toolIds$ = this.res$["includedIds"];
    this.toolImages$ = this.res$["includedImages"];
    this.numOfTools = new Array(this.toolNames$.length);
    this.preloader = false;
  }


  ngDoCheck(): void {
    // alert('doCheck');
  }

}
