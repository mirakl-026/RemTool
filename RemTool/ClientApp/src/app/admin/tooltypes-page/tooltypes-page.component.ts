import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { DataService } from '../../DataService/data.service';
import { ToolType } from '../../DataService/toolType';
import { FormControl, FormGroup } from '@angular/forms';


@Component({
  selector: 'tooltypes-page',
  templateUrl: './tooltypes-page.component.html',
  styleUrls: ['./tooltypes-page.component.scss'],
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
    this.addToolType();
    this.toolType = tt;
    for (let i of tt.serves) {
      this.serveCostLength.push(0);
    }
  }

  // сброс инструмента
  resetToolType() {
    this.toolType = new ToolType();
    this.addToolFlag = false;
    this.serveCostLength = [];
  }

  // удаление инструмента 
  deleteToolType(tt: ToolType) {
    this.dataService.deleteToolType(tt.id)
      .subscribe(data => this.loadToolTypes());
  }

  // добавление инструмента
  addToolType() {
    this.resetToolType();
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

  // Добавление строки в прайслист
  newKey: string = "";
  newValue: string = "";
  serveCostLength: number[] = [];
  addRowToPrice() {
    if ((this.newKey != "") && (this.newValue != "")) {
      this.toolType.serves.push(this.newKey);
      this.toolType.costs.push(this.newValue);
      this.newKey = "";
      this.newValue = "";
      this.serveCostLength.push(0);
    }
  }

  // Удаление строки из прайслиста 
  removeRow(i) {
    this.toolType.serves.splice(i, 1);
    this.toolType.costs.splice(i, 1);
    this.serveCostLength.pop();
  }
  chooseImgPop: boolean = false;
  images: string = "";
  chooseImg(a) {
    this.chooseImgPop = a;
    if (a) {
      this.http.get("api/images/getimages").subscribe((data: string) => this.images = data);
    }
  }

  // Открыть попап с картинками
  popupClick(e) {
    if (e.target == document.querySelector(".popup__body")) {
      this.chooseImg(false);
    }
  }

  // Добавиить картинку к инструменту
  addImage(e, img) {
    console.log(String(e.target.currentSrc));
    this.toolType.imgRefenrence = String(img);
    this.chooseImg(false);
  }

  // Выбор картинки дл язагрузки на сервер
  selectedFile: File = null;
  onSelectFile(fileInput: any) {
    this.selectedFile = <File>fileInput.target.files[0];
  }

  // Отправка картинки на сервер
  onSubmit(data) {
    const formData = new FormData();
    formData.append('newImage', this.selectedFile);
    this.http.post('api/images/addImage', formData)
      .subscribe(res => {
        alert('Uploaded!!');
        this.http.get("api/images/getimages").subscribe((data: string) => this.images = data);
      });
    this.newImageForm.reset();
  }

  // Удаление картинки с сервера
  deleteImage(event) {
    let imgSrc: string = event.target.parentNode.querySelector('img').src;
    let reg =  /images\//;
    let fileNameIndex: number = imgSrc.match(reg).index + 7;
    let fileName: string = imgSrc.slice(fileNameIndex);
    this.http.delete("api/images/DeleteImage/" + fileName).subscribe(data => this.chooseImg(true));
  }



}

