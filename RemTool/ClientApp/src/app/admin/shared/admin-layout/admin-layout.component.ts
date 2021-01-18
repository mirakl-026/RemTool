import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DataService } from 'src/app/DataService/data.service';
import { AuthService } from 'src/app/shared/auth.service';

@Component({
  selector: 'app-admin-layout',
  templateUrl: './admin-layout.component.html',
  styleUrls: ['./admin-layout.component.scss']
})
export class AdminLayoutComponent implements OnInit {

  constructor(
    public auth: AuthService,
    private router: Router,
    private dataService: DataService,
    private http: HttpClient
  ) { }

  navMobile;
  burger;

  ngOnInit(): void {
    window.addEventListener('resize', () => {
      if (window.matchMedia("(max-width: 683.98px)").matches) {
        this.navMobile = true;
        this.burger = false;
      } else {
        this.navMobile = false;
        this.burger = true;
      }
    });
    
    if (window.matchMedia("(max-width: 683.98px)").matches) {
      this.navMobile = true;
      this.burger = false;
    } else {
      this.navMobile = false;
      this.burger = true;
    }
  }
  logout($event) {
    event.preventDefault();
    this.auth.logout();
    this.router.navigate(['/admin', 'login']);
    this.burger = false;
  }

}
