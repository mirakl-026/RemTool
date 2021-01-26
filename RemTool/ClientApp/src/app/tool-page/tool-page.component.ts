import { JsonPipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { DataService } from '../DataService/data.service';

@Component({
  selector: 'app-tool-page',
  templateUrl: './tool-page.component.html',
  styleUrls: ['./tool-page.component.scss'],
  providers: [DataService]
})
export class ToolPageComponent implements OnInit {
  private destroy$ = new Subject<undefined>();

  constructor(
    private route: ActivatedRoute,
    private dataService: DataService,
    private http: HttpClient
  ) { }

  preloader: boolean = false;
  id: string;
  res$;
  pricelist$: string[] = [];
  titleName$;

  ngOnInit(): void {
    this.route.params.pipe(
      takeUntil(this.destroy$))
      .subscribe(params => {
        this.id = params.id;
        console.log(this.id);
        this.preloader = true;
        this.getTool(this.id);
      });
  }

  getTool(id) {
    this.dataService.getToolType(id).subscribe(data => {
      this.res$ = data;
      console.log(this.res$);
      if (!this.res$.nameSeo) {
        document.title = `Ремонт | ${this.res$.name.toLowerCase()}`;
      } else {
        document.title = `Ремонт ${this.res$.nameSeo.toLowerCase()}`;
      }
      // this.http.get(`http://ws3.morpher.ru/russian/declension?s=${this.res$.name}&format=json`).subscribe(res => {
      //   this.titleName$ = res;
      //   console.log(this.titleName$['Р']);
      //   document.title = `Ремонт ${this.titleName$['Р'].toLowerCase()}`;
      // });
      this.res$.serves.unshift('Вид работ');
      this.res$.costs.unshift('Стоимость, руб');
      this.pricelist$ = [];
      this.pricelist$.push('Вид работ');
      this.pricelist$.push('Стоимость, руб');
      for (let i = 0; i < this.res$.serves.length; i++) {
        // this.pricelist$.push(this.res$.serves[i]);
        // this.pricelist$.push(this.res$.costs[i]);
      }
    });
  }
}
