var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { Component } from '@angular/core';
import { DataService } from './data.service';
import { Brand } from './brand';
import { Tool } from './tool';
import { SparePart } from './sparepart';
let AppComponent = class AppComponent {
    constructor(dataService) {
        this.dataService = dataService;
        // Brands
        this.brand = new Brand(); // изменяемый бренд
        this.tableModeBrand = true; // табличный режим
        // Tools
        this.tool = new Tool();
        this.tableModeTool = true; // табличный режим
        // SpareParts
        this.sparePart = new SparePart();
        this.tableModeSparePart = true; // табличный режим
    }
    ngOnInit() {
        this.loadBrands(); // загрузка данных при старте компонента  
        this.loadTools();
        this.loadSpareParts();
    }
    // Brands
    // получаем данные через сервис
    loadBrands() {
        this.dataService.getBrands()
            .subscribe((data) => this.brands = data);
    }
    // сохранение данных
    saveBrand() {
        if (this.brand.id == null) {
            this.dataService.createBrand(this.brand)
                .subscribe((data) => this.brands.push(data));
        }
        else {
            this.dataService.updateBrand(this.brand)
                .subscribe(data => this.loadBrands());
        }
        this.cancelBrand();
    }
    editBrand(b) {
        this.brand = b;
    }
    cancelBrand() {
        this.brand = new Brand();
        this.tableModeBrand = true;
    }
    deleteBrand(b) {
        this.dataService.deleteBrand(b.id)
            .subscribe(data => this.loadBrands());
    }
    addBrand() {
        this.cancelBrand();
        this.tableModeBrand = false;
    }
    // Tools
    // получаем данные через сервис
    loadTools() {
        this.dataService.getTools()
            .subscribe((data) => this.tools = data);
    }
    // сохранение данных
    saveTool() {
        if (this.tool.id == null) {
            this.dataService.createTool(this.tool)
                .subscribe((data) => this.tools.push(data));
        }
        else {
            this.dataService.updateTool(this.tool)
                .subscribe(data => this.loadTools());
        }
        this.cancelTool();
    }
    editTool(t) {
        this.tool = t;
    }
    cancelTool() {
        this.tool = new Tool();
        this.tableModeTool = true;
    }
    deleteTool(t) {
        this.dataService.deleteTool(t.id)
            .subscribe(data => this.loadTools());
    }
    addTool() {
        this.cancelTool();
        this.tableModeTool = false;
    }
    // SpareParts
    // получаем данные через сервис
    loadSpareParts() {
        this.dataService.getSpareParts()
            .subscribe((data) => this.spareParts = data);
    }
    // сохранение данных
    saveSparePart() {
        if (this.sparePart.id == null) {
            this.dataService.createSparePart(this.sparePart)
                .subscribe((data) => this.spareParts.push(data));
        }
        else {
            this.dataService.updateSparePart(this.sparePart)
                .subscribe(data => this.loadSpareParts());
        }
        this.cancelSparePart();
    }
    editSparePart(sp) {
        this.sparePart = sp;
    }
    cancelSparePart() {
        this.sparePart = new SparePart();
        this.tableModeSparePart = true;
    }
    delete(sp) {
        this.dataService.deleteSparePart(sp.id)
            .subscribe(data => this.loadSpareParts());
    }
    addSparePart() {
        this.cancelSparePart();
        this.tableModeSparePart = false;
    }
};
AppComponent = __decorate([
    Component({
        selector: 'app',
        templateUrl: './app.component.html',
        providers: [DataService]
    })
], AppComponent);
export { AppComponent };
//# sourceMappingURL=app.component.js.map