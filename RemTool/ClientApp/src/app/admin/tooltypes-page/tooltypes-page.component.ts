import { Component, OnInit } from '@angular/core';

import { DataService } from '../DataService/data.service';
import { ToolType } from '../DataService/toolType';


@Component({
  selector: 'tooltypes-page',
  templateUrl: './tooltypes-page.component.html',
  styleUrls: ['./tooltypes-page.component.scss'],
  providers: [DataService]
})
export class ToolTypePageComponent implements OnInit {

  // ToolTypes
  toolType: ToolType = new ToolType();
  toolTypes: ToolType[];
  tableModeToolType: boolean = true;      // табличный режим

  constructor(private dataService: DataService) { }

  ngOnInit(): void {
    this.loadToolTypes();
  }

  // ToolTypes
  // получаем данные через сервис
  loadToolTypes() {
    this.dataService.getToolTypes()
      .subscribe((data: ToolType[]) => this.toolTypes = data);
  }

  // сохранение инструмента
  saveToolType() {
    if (this.toolType.id == null) {
      this.dataService.createToolType(this.toolType)
        .subscribe((data: ToolType) => this.toolTypes.push(data));
    } else {
      this.dataService.updateToolType(this.toolType)
        .subscribe(data => this.loadToolTypes());
    }
    this.resetToolType();
  }

  // редактирование инструмента
  editToolType(tt: ToolType) {
    this.toolType = tt;
  }

  // сброс
  resetToolType() {
    this.toolType = new ToolType();
    this.tableModeToolType = true;
  }

  // удаление инструмента 
  deleteToolType(tt: ToolType) {
    this.dataService.deleteToolType(tt.id)
      .subscribe(data => this.loadToolTypes());
  }

  // добавление инструмента
  addToolType() {
    this.resetToolType();
    this.tableModeToolType = false;
  }

  // добавление бренда
  currentBrand: string = "";

  addBrandToToolType() {
    if (this.currentBrand != "") {
      if (this.toolType.brands == null)
        this.toolType.brands = [];

      this.toolType.brands.push(this.currentBrand);
      this.currentBrand = "";
    }    
  }

  // добавление пары - услуга+стоимость
  addServeCostToToolType(serve: string, cost: string) {
    this.toolType.serveCost.keys.push(serve);
    this.toolType.serveCost.values.push(cost);
  }


}

