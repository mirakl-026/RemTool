import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
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
    resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<ToolType> | Promise<ToolType> | ToolType {
    (console.log(route));
    this.type = String(route.params["type"]);
    console.log(this.type);
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
      // let id = route.queryParams['id'];
      this.res = this.dataService.getToolType(route.params['id']);
    }
    // console.log(tool);

    // const observable: Observable<ToolType> = Observable.create(observer => {
    //   observer.next(tool);
    //   observer.complete();
    // });

    return this.res;
  }
}