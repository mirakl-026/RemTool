import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SparePart } from './sparepart';
import { ToolType } from './toolType';

@Injectable()
export class DataService {
  private urlSpareParts = "api/spareparts";
  private urlToolTypes = "api/tooltypes";
  private urlToolTypePrice = "api/tooltypes/GetPriceList";

  constructor(private http: HttpClient) {

  }

  getImages(){
    return this.http.get(this.urlToolTypes + '/' + "GetImages");
  }


  // Всё что касается инструментов
  //[HttpGet("GetElectroTools")]
  //public string GetElectroTools()
  getElectroTools() {
    return this.http.get(this.urlToolTypes + '/' + "GetElectroTools");
  }

  //[HttpGet("GetFuelTools")]
  //public string GetFuelTools()
  getFuelTools() {
    return this.http.get(this.urlToolTypes + '/' + "GetFuelTools");
  }

  //[HttpGet("GetWeldingTools")]
  //public string GetWeldingTools()
  getWeldingTools() {
    return this.http.get(this.urlToolTypes + '/' + "GetWeldingTools");
  }

  //[HttpGet("GetGenerators")]
  //public string GetGenerators()
  getGenerators() {
    return this.http.get(this.urlToolTypes + '/' + "GetWeldingTools");
  }

  //[HttpGet("GetCompressors")]
  //public string GetCompressors()
  getCompressors() {
    return this.http.get(this.urlToolTypes + '/' + "GetCompressors");
  }

  //[HttpGet("GetRestTools")]
  //public string GetRestTools()
  getRestTools() {
    return this.http.get(this.urlToolTypes + '/' + "GetRestTools");
  }

  //[HttpGet("GetGardenTools")]
  //public string GetGardenTools()
  getGardenTools() {
    return this.http.get(this.urlToolTypes + '/' + "GetGardenTools");
  }

  //[HttpGet("GetHeatGuns")]
  //public string GetHeatGuns()
  getHeatGuns() {
    return this.http.get(this.urlToolTypes + '/' + "GetHeatGuns");
  }

  //[HttpGet("GetPriceList")]
  //public string GetPriceList(string id)
  getPriceListById(id: string) {
    return this.http.get(this.urlToolTypePrice + '/' + id);
  }

  //[HttpGet("GetPriceList")]
  //public string GetPriceList(int mainType, int secondType)
  getPriceListByType(mainType: number, secondType: number) {
    return this.http.get(this.urlToolTypePrice + '?' + 'mainType=' + mainType + '&secondType=' + secondType);
  }

  //[HttpGet("GetPriceListByFilter")]
  //public string GetPriceListByFilter(string filter)
  getPriceListByFilter(filter: string) {
    return this.http.get(this.urlToolTypePrice + '?' + 'filter=' + filter);
  }

  //[HttpGet]
  //public IEnumerable<ToolType> Get()
  getToolTypes() {
    return this.http.get(this.urlToolTypes);
  }

  //[HttpGet("{id}")]
  //public ToolType Get(string id)
  getToolType(id: string) {
    return this.http.get(this.urlToolTypes + '/' + id);
  }

  //[HttpPost]
  //public IActionResult Post(ToolType toolType)
  createToolType(toolType: ToolType) {
    return this.http.post(this.urlToolTypes, toolType);
  }

  //[HttpPut]
  //public IActionResult Put(ToolType toolType)
  updateToolType(toolType: ToolType) {
    return this.http.put(this.urlToolTypes, toolType);
  }

  //[HttpDelete("{id}")]
  //public IActionResult Delete(string id)
  deleteToolType(id: string) {
    return this.http.delete(this.urlToolTypes + '/' + id);
  }

  //[HttpDelete("DeleteAllToolTypes")]
  //public IActionResult DeleteAll()
  deleteToolTypes() {
    return this.http.delete(this.urlToolTypes + '/' + "DeleteAllToolTypes");
  }

    
  // Запчасти
  // spareParts
  getSpareParts() {
    return this.http.get(this.urlSpareParts);
  }

  getSparePart(id: string) {
    return this.http.get(this.urlSpareParts + '/' + id);
  }

  createSparePart(sparePart: SparePart) {
    return this.http.post(this.urlSpareParts, sparePart);
  }

  updateSparePart(sparePart: SparePart) {
    return this.http.put(this.urlSpareParts, sparePart);
  }

  deleteSparePart(id: string) {
    return this.http.delete(this.urlSpareParts + '/' + id);
  }

  // картинки

}
