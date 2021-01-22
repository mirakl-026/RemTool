import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-all-tools-page',
  templateUrl: './all-tools-page.component.html',
  styleUrls: ['./all-tools-page.component.scss']
})
export class AllToolsPageComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
    document.title = 'Ремонт строительного инструмента';
  }

}
