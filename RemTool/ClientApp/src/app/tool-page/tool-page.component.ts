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
    private dataService: DataService
  ) { }

  preloader: boolean = false;
  id: string;
  res$;
  pricelist$: string[] = [];

  ngOnInit(): void {
    this.route.params.pipe(
      takeUntil(this.destroy$))
      .subscribe(params => {
        window.scrollTo({
          top: 0,
          behavior: "smooth"
        }); 
        this.id = params.id;
        console.log(this.id);
        this.preloader = true;
        this.getTool(this.id);
      });
  }

  getTool(id) {
    this.dataService.getToolType(id).subscribe(data => {
      this.res$ = data;
      for (let i = 0; i < this.res$.serves.length; i++) {
        this.pricelist$.push(this.res$.serves[i]);
        this.pricelist$.push(this.res$.costs[i]);
      }
    });
  }
}
