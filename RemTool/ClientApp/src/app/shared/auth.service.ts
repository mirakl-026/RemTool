import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { environment } from 'src/environments/environment';
import { tap } from 'rxjs/operators';
import { DataService } from '../DataService/data.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private urlAuth = "api/auth/login";

  //user1 = new User("admin@gmail.com", "admin");

  constructor(
    private http: HttpClient,
    private dataService: DataService
    ) { }

  login( User ) {
    //return this.http.post(`https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=${environment.apiKey}`, User)
    return this.http.post(this.urlAuth, User)
    .pipe(
      tap(this.setToken)
    );
    
  }

  private setToken(response) {
    if (response) {
      const expDate = new Date( new Date().getTime() + + response.expiresIn * 10000);
      localStorage.setItem('token-exp', expDate.toString())
      localStorage.setItem('token', response.idToken)
    } else {
      localStorage.clear();
    }
  }
  get token() {
    const expDate = new Date(localStorage.getItem('token-exp'));
    if ( new Date > expDate ) {
      this.logout();
      return null;
    }
    return localStorage.getItem('token');
  }

  logout() {
    this.setToken(null);
  }

  isToken () {
    return !!this.token;
  }
  isAuth(): number {
    let status: number;
    this.dataService.checkAuth().subscribe(data => {
      status = data["status"];
    })
    return status;
  }
}
