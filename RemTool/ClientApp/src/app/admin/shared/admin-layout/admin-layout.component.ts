import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/shared/auth.service';

@Component({
  selector: 'app-admin-layout',
  templateUrl: './admin-layout.component.html',
  styleUrls: ['./admin-layout.component.scss']
})
export class AdminLayoutComponent implements OnInit {

  constructor(
    public auth: AuthService,
    private router: Router
  ) { }

  ngOnInit(): void {
  }
  logout($event) {
    event.preventDefault();
    this.auth.logout();
    this.router.navigate(['/admin', 'login']);
  }

}
