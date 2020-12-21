import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { DataService } from '../DataService/data.service';
import { ToolType } from '../DataService/toolType';

@Injectable({
  providedIn: 'root',
  // providers: [DataService]
})



export class ResolverService implements Resolve<ToolType> {
  constructor(
    private dataService: DataService
    ) {  }
    
    res;
    type;
    tools: MainTools = new MainTools();
    resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Promise<any> | Observable<any> | any | Observable<ToolType> | Promise<ToolType> | ToolType | Observable<MainTools> | MainTools | Promise<MainTools> | Observable<object> | ToolType[]{
    this.type = String(route.params["type"]);
    if (route.routeConfig["path"] == "tools/:type") {
      if (this.type == "electro") {
        this.res = this.dataService.getElectroTools();
      } else if (this.type == "benzo") {
        this.res = this.dataService.getFuelTools();
      } else if (this.type == "garden") {
        this.res = this.dataService.getGardenTools();
      } else if (this.type == "compressor") {
        this.res = this.dataService.getCompressors();
      } else if (this.type == "generator") {
        this.res = this.dataService.getGenerators();
      } else if (this.type == "welding") {
        this.res = this.dataService.getWeldingTools();
      } else if (this.type == "heatgun") {
        this.res = this.dataService.getHeatGuns();
      } else if (this.type == "rest") {
        this.res = this.dataService.getRestTools();
      }
    } else if (route.routeConfig["path"] == "tools/:type/:id") {
      this.res = this.dataService.getToolType(route.params['id']);
    } else if (route.routeConfig["path"] == "") {
      // this.tools.electro = this.dataService.getElectroTools();
      this.dataService.getElectroTools().subscribe(data => {
        this.tools.electro = data;
      });
      this.dataService.getFuelTools().subscribe(data => {
        this.tools.benzo = data;
      });
      this.dataService.getGardenTools().subscribe(data => {
        this.tools.garden = data;
      });
      this.dataService.getCompressors().subscribe(data => {
        this.tools.compressors = data;
      });
      this.dataService.getGenerators().subscribe(data => {
        this.tools.generators = data;
      });
      this.dataService.getWeldingTools().subscribe(data => {
        this.tools.welding = data;
      });
      this.dataService.getHeatGuns().subscribe(data => {
        this.tools.heatguns = data;
      });
      this.dataService.getRestTools().subscribe(data => {
        this.tools.rest = data;
      });
      this.res = this.dataService.getElectroTools();
      return this.tools;
    }
    return this.res;
  }
}

export class MainTools {
  constructor (
    public electro?: any,
    public benzo?: any,
    public garden?: any,
    public compressors?: any,
    public generators?: any,
    public welding?: any,
    public heatguns?: any,
    public rest?: any
  ){
    this.electro = {};
    this.benzo = {};
    this.garden = {};
    this.compressors = {};
    this.generators = {};
    this.welding = {};
    this.heatguns = {};
    this.rest = {};
  }
}