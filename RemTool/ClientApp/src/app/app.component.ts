import { Component, OnInit } from '@angular/core';
import { DataService } from './data.service';
import { Brand } from './brand';
import { Tool } from './tool';
import { SparePart } from './sparepart';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    providers: [DataService]
})
export class AppComponent implements OnInit {

    // Brands
    brand: Brand = new Brand();     // изменяемый бренд
    brands: Brand[];                // массив брендов

    // Tools
    tool: Tool = new Tool();
    tools: Tool[];

    // SpareParts
    sparePart: SparePart = new SparePart();
    spareParts: SparePart[];

    tableMode: boolean = true;      // табличный режим



    constructor(private dataService: DataService) { }

    ngOnInit() {
        this.loadBrands();    // загрузка данных при старте компонента  
        this.loadTools();
        this.loadSpareParts();
    }

    // Brands
    // получаем данные через сервис
    loadBrands() {
        this.dataService.getBrands()
            .subscribe((data: Brand[]) => this.brands = data);
    }
    // сохранение данных
    saveBrand() {
        if (this.brand.id == null) {
            this.dataService.createBrand(this.brand)
                .subscribe((data: Brand) => this.brands.push(data));
        } else {
            this.dataService.updateBrand(this.brand)
                .subscribe(data => this.loadBrands());
        }
        this.cancelBrand();
    }
    editBrand(b: Brand) {
        this.brand = b;
    }
    cancelBrand() {
        this.brand = new Brand();
        this.tableMode = true;
    }
    deleteBrand(b: Brand) {
        this.dataService.deleteBrand(b.id)
            .subscribe(data => this.loadBrands());
    }
    addBrand() {
        this.cancelBrand();
        this.tableMode = false;
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
        this.tableMode = true;
    }
    deleteTool(t: Tool) {
        this.dataService.deleteTool(t.id)
            .subscribe(data => this.loadTools());
    }
    addTool() {
        this.cancelTool();
        this.tableMode = false;
    }


    // SpareParts
    // получаем данные через сервис
    loadSpareParts() {
        this.dataService.getSpareParts()
            .subscribe((data: SparePart[]) => this.spareParts = data);
    }
    // сохранение данных
    saveSparePart() {
        if (this.sparePart.id == null) {
            this.dataService.createSparePart(this.sparePart)
                .subscribe((data: SparePart) => this.spareParts.push(data));
        } else {
            this.dataService.updateSparePart(this.sparePart)
                .subscribe(data => this.loadSpareParts());
        }
        this.cancelSparePart();
    }
    editSparePart(sp: SparePart) {
        this.sparePart = sp;
    }
    cancelSparePart() {
        this.sparePart = new SparePart();
        this.tableMode = true;
    }
    delete(sp: SparePart) {
        this.dataService.deleteSparePart(sp.id)
            .subscribe(data => this.loadSpareParts());
    }
    add() {
        this.cancelSparePart();
        this.tableMode = false;
    }    
}