import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { environment } from 'src/environments/environment';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  login( User ) {
    return this.http.post(`https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=${environment.apiKey}`, User)
    .pipe(
      tap(this.setToken)
    )
  }

  private setToken(response) {
    if (response) {
      const expDate = new Date( new Date().getTime() + +response.expiresIn * 1000);
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

  isAuth () {
    return !!this.token;
  }
}
