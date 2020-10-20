import { Component, OnInit } from '@angular/core';

import { DataService } from '../DataService/data.service';
import { SparePart } from '../DataService/sparepart';

@Component({
  selector: 'spareparts-page',
  templateUrl: './spareparts-page.component.html',
  styleUrls: ['./spareparts-page.component.scss'],
  providers: [DataService]
})
export class SparePartsPageComponent implements OnInit {

  // SpareParts
  sparePart: SparePart = new SparePart();
  spareParts: SparePart[];
  tableModeSparePart: boolean = true;      // табличный режим

  constructor(private dataService: DataService) { }

  ngOnInit(): void {
    this.loadSpareParts();
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
      this.tableModeSparePart = true;
  }
  deleteSparePart(sp: SparePart) {
      this.dataService.deleteSparePart(sp.id)
          .subscribe(data => this.loadSpareParts());
  }
  addSparePart() {
      this.cancelSparePart();
      this.tableModeSparePart = false;
  }    

}
