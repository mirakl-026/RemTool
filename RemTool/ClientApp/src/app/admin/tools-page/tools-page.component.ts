import { Component, OnInit } from '@angular/core';
import { from } from 'rxjs';

import { DataService } from '../DataService/data.service';
import { Tool } from '../DataService/tool';

@Component({
  selector: 'tools-page',
  templateUrl: './tools-page.component.html',
  styleUrls: ['./tools-page.component.scss'],
  providers: [DataService]
})
export class ToolsPageComponent implements OnInit {

  // Tools
  tool: Tool = new Tool();
  tools: Tool[];
  tableModeTool: boolean = true;      // табличный режим

  constructor(private dataService: DataService) { }

  ngOnInit(): void {
    this.loadTools();
  }

  // Tools
    // получаем данные через сервис
    loadTools() {
      this.dataService.getTools()
          .subscribe((data: Tool[]) => this.tools = data);
  }
  // сохранение данных
  saveTool() {
      if (this.tool.id == null) {
          this.dataService.createTool(this.tool)
              .subscribe((data: Tool) => this.tools.push(data));
      } else {
          this.dataService.updateTool(this.tool)
              .subscribe(data => this.loadTools());
      }
      this.cancelTool();
  }
  editTool(t: Tool) {
      this.tool = t;
  }
  cancelTool() {
      this.tool = new Tool();
      this.tableModeTool = true;
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
