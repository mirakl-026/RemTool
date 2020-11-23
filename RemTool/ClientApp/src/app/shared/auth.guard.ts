import { HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router, Resolve } from '@angular/router';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { DataService } from '../DataService/data.service';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(
    private auth: AuthService,
    private dataService: DataService,
    private router: Router
  ){}

  // canActivate(
  //   route: ActivatedRouteSnapshot,
  //   state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
  //   return this.dataService.checkAuth().pipe(map(
  //     res => {

  //     }
  //   ))

  // }
  
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    return this.dataService.checkAuth().pipe(
      map(res => {
        if (res["status"] == 200) {
          return true;
        } else {
          this.router.navigate(['/admin', 'login']);
          return false;
        }
      })
    );
  }

}
