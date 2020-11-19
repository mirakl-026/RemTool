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
    
    resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<ToolType> | Promise<ToolType> | ToolType {
    let id = route.queryParams['id'];
    let tool = this.dataService.getToolType(id);
    // console.log(tool);

    // const observable: Observable<ToolType> = Observable.create(observer => {
    //   observer.next(tool);
    //   observer.complete();
    // });

    return tool;
  }
}