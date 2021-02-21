import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { DataService } from '../../DataService/data.service';
import { ToolType } from '../../DataService/toolType';
import { FormControl, FormGroup } from '@angular/forms';
import { AuthService } from 'src/app/shared/auth.service';


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
    private http: HttpClient,
    private auth: AuthService
  ) { }

  newImageForm: FormGroup;
  newKey: string = "";
  newValue: string = "";
  serveCostLength: number[] = [];

  deletePopupFlags: boolean[] = [];

  ngOnInit(): void {
    this.loadToolTypes();
    this.newImageForm = new FormGroup({
      newImage: new FormControl(null)
    });
  }

  // ToolTypes
  // получаем данные через сервис
  loadToolTypes() {
    this.deletePopupFlags = [];
    this.dataService.getToolTypes()
      .subscribe((data: ToolType[]) => {
        this.toolTypes = data;
        for (let i of this.toolTypes) {
          this.deletePopupFlags.push(false);
        }
      });
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
    this.dataService.getToolType(tt.id).subscribe(data => this.toolType = data);
    for (let i of tt.serves) {
      this.serveCostLength.push(0);
    }

    // window.scrollTo({
    //   top: document.querySelector('.create').getBoundingClientRect().top,
    //   behavior: "smooth"
    // });

  }

  // сброс картинки
  imgReset() {
    this.toolType.imgRefenrence = "";
  }

  // сброс инструмента
  resetToolType() {
    this.newKey = '';
    this.newValue = '';
    this.toolType = new ToolType();
    this.addToolFlag = false;
    this.serveCostLength = [];
    this.deletePopupFlags = [];
  }

  // удаление инструмента 
  deleteToolType(tt: ToolType) {
    this.dataService.deleteToolType(tt.id)
      .subscribe(data => this.loadToolTypes());
  }

  deletePopup(i){
    this.deletePopupFlags[i] = true;
  }

  // добавление инструмента
  addToolType() {
    this.resetToolType();
    this.addToolFlag = true;
    window.scrollTo({
      top: 0,
      behavior: 'smooth'
    })
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

  addRowToPrice() {
    if ((this.newKey != "") && (this.newValue != "")) {
      this.toolType.serves.push(this.newKey);
      this.toolType.costs.push(this.newValue);
      this.newKey = "";
      this.newValue = "";
      this.serveCostLength.push(0);
    } else if ((this.newKey != "") && (this.newValue == "")) {
      this.toolType.serves.push(this.newKey);
      this.toolType.costs.push("<h4>");
      this.newKey = "";
      this.newValue = "";
      this.serveCostLength.push(0);
    }
  }

  rowUp(i) {
    if (i > 0) {
      let upperServe = this.toolType.serves[i - 1];
      let upperCost = this.toolType.costs[i - 1];
      this.toolType.serves[i - 1] = this.toolType.serves[i];
      this.toolType.costs[i - 1] = this.toolType.costs[i];
      this.toolType.serves[i] = upperServe;
      this.toolType.costs[i] = upperCost;
    } else return;
  }

  rowDown(i) {
    if (i < this.toolType.serves.length - 1) {
      let bottomServe = this.toolType.serves[i + 1];
      let bottomCost = this.toolType.costs[i + 1];
      this.toolType.serves[i + 1] = this.toolType.serves[i];
      this.toolType.costs[i + 1] = this.toolType.costs[i];
      this.toolType.serves[i] = bottomServe;
      this.toolType.costs[i] = bottomCost;
    } else return;
  }

  removeBrand(i) {
    this.toolType.brands.splice(i, 1);
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
      // this.dataService.getImages();
    }
  }

  // Открыть попап с картинками
  popupClick(e) {
    if (e.target == document.querySelector(".popup__body")) {
      this.chooseImg(false);
    }
    this.auth.isAuth();
  }

  // Добавиить картинку к инструменту
  addImage(e, img) {
    this.toolType.imgRefenrence = String(img);
    this.chooseImg(false);
  }

  // Выбор картинки для загрузки на сервер
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

  refreshSearch() {
    this.http.patch("/api/tooltype/RefreshAllToolTypesSearch", null).subscribe();
  }
}