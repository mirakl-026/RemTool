import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-settings-page',
  templateUrl: './settings-page.component.html',
  styleUrls: ['./settings-page.component.scss']
})
export class SettingsPageComponent implements OnInit {

  constructor(
    private http: HttpClient
  ) { }

  ngOnInit(): void {
  }
  createBackup() {
    this.http.get("api/Backup/PackToZip").subscribe();

  }
  restoreSite(){
    this.http.get("api/Backup/UnpackFromZip").subscribe();
  }


}
