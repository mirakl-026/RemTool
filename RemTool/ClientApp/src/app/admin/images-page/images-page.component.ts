import { Component, OnInit } from '@angular/core';
import { from } from 'rxjs';

import { DataService } from '../DataService/data.service';
import { Image } from '../DataService/image';

@Component({
  selector: 'tools-page',
  templateUrl: './tools-page.component.html',
  styleUrls: ['./tools-page.component.scss'],
  providers: [DataService]
})
export class ToolsPageComponent implements OnInit {

  // Images
  image: Image = new Image();
  tools: Image[];
  tableModeTool: boolean = true;      // табличный режим

  constructor(private dataService: DataService) { }

  ngOnInit(): void {
    this.loadImages();
  }

  // Tools
  // получаем данные через сервис
  loadImages() {
    this.dataService.getImages()
      .subscribe((data: string[]) => this.tools = data);
  }

  deleteTool(t: Tool) {
    this.dataService.deleteTool(t.id)
      .subscribe(data => this.loadTools());
  }
  addTool() {
    this.cancelTool();
    this.tableModeTool = false;
  }

}
