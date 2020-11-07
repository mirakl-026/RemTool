import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { DataService } from '../DataService/data.service';
import { ToolType } from '../DataService/toolType';
import { FormControl, FormGroup } from '@angular/forms';


@Component({
  selector: 'tooltypes-page',
  templateUrl: './tooltypes-page.component.html',
  styleUrls: [
    './tooltypes-page.component.scss',
    './editor.scss'
  ],
  providers: [DataService]
})
export class ToolTypePageComponent implements OnInit {

  // ToolTypes
  toolType: ToolType = new ToolType();
  toolTypes: ToolType[] = [];
  // tableModeToolType: boolean = true;      // табличный режим
  addToolFlag: boolean = false;           // флаг добавления нового инструмента
  categories: string[] = [
    'Электроинструмент',
    'Бензоинструмент',
    'Садовая техника',
    'Компрессоры',
    'Генераторы',
    'Сварочная техника',
    'Тепловые пушки',
    'Техника для отдыха'
  ]

  constructor(
    private dataService: DataService,
    private http: HttpClient
  ) { }

  newImageForm: FormGroup;
  
  ngOnInit(): void {
    this.loadToolTypes();
    this.newImageForm = new FormGroup({
      newImage: new FormControl(null)
    });


  }

  // ToolTypes
  // получаем данные через сервис
  loadToolTypes() {
    this.dataService.getToolTypes()
      .subscribe((data: ToolType[]) => this.toolTypes = data);
  }

  // сохранение инструмента
  /*
  saveToolType() {
    console.log(this.toolType)
    if (this.toolType.id == null) {
      this.dataService.createToolType(this.toolType)
      .subscribe((data: ToolType) => this.toolTypes.push(data));
    } else {
      this.dataService.updateToolType(this.toolType)
      .subscribe(data => this.loadToolTypes());
    }
    this.resetToolType();
  }*/



  saveToolType() {
    this.toolTypes.push(this.toolType);
    this.toolType = new ToolType();
    console.log(this.toolTypes);
    this.addToolFlag = false;

  }

  // редактирование инструмента
  editToolType(tt: ToolType) {
    this.toolType = tt;
  }

  // сброс
  resetToolType() {
    this.toolType = new ToolType();
    // this.tableModeToolType = true;
    this.addToolFlag = false;
  }

  // удаление инструмента 
  deleteToolType(tt: ToolType) {
    this.dataService.deleteToolType(tt.id)
      .subscribe(data => this.loadToolTypes());
  }

  // добавление инструмента
  addToolType() {
    this.resetToolType();
    // this.tableModeToolType = false;
    this.addToolFlag = true;
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

  newKey: string = "";
  newValue: string = "";
  serveCostLength: number[] = [];

  addRowToPrice() {
    if ((this.newKey != "") && (this.newValue != "")) {
      this.toolType.serveCost.keys.push(this.newKey);
      this.toolType.serveCost.values.push(this.newValue);
      this.newKey = "";
      this.newValue = "";
      this.serveCostLength.push(0);
    }
    // console.log(this.toolType.serveCost.keys);
    // console.log(this.toolType.serveCost.values);
  }

  removeRow(i) {
    this.toolType.serveCost.keys.splice(i, 1);
    this.toolType.serveCost.values.splice(i, 1);
    this.serveCostLength.pop();
  }

  chooseImgPop: boolean = false;

  images: string = "";
  chooseImg(a) {
    this.chooseImgPop = a;
    // console.log(this.dataService.getImages());
    if (a) {
      this.http.get("api/images/getimages").subscribe((data: string) => this.images = data);
    }
    console.log(this.images);

    //http.get запрос для получения всех картинок на сервере

  }

  popupClick(e) {
    if (e.target == document.querySelector(".popup__body")) {
      this.chooseImg(false);
    }
    // console.log(e.target);
  }

  addImage(e) {
    console.log(String(e.target.currentSrc));
    this.toolType.imgRefenrence = String(e.target.currentSrc);
    this.chooseImg(false);
  }

  selectedFile: File = null;

  onSelectFile(fileInput: any) {
    this.selectedFile = <File>fileInput.target.files[0];
  }

  onSubmit(data) {

    const formData = new FormData();
    formData.append('newImage', this.selectedFile);

    this.http.post('api/images/addImage', formData)
      .subscribe(res => {

        alert('Uploaded!!');
      });

    this.newImageForm.reset();
  }
}

