import { Component, OnInit } from '@angular/core';
import { DataService } from '../DataService/data.service';
import { Brand } from '../DataService/brand';

@Component({
  selector: 'brands-page',
  templateUrl: './brands-page.component.html',
  styleUrls: ['./brands-page.component.scss'],
  providers: [DataService]
})
export class BrandsPageComponent implements OnInit {

  // Brands
  brand: Brand = new Brand();     // изменяемый бренд
  brands: Brand[];                // массив брендов
  tableModeBrand: boolean = true;      // табличный режим

  constructor(private dataService: DataService) { }

  ngOnInit(): void {
    this.loadBrands();    // загрузка данных при старте компонента  
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
      this.tableModeBrand = true;
  }
  deleteBrand(b: Brand) {
      this.dataService.deleteBrand(b.id)
          .subscribe(data => this.loadBrands());
  }
  addBrand() {
      this.cancelBrand();
      this.tableModeBrand = false;
  }
}
