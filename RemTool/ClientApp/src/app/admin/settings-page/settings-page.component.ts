import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { DataService } from '../../DataService/data.service';
import { ToolType } from '../../DataService/toolType';
import { FormControl, FormGroup } from '@angular/forms';
import { AuthService } from 'src/app/shared/auth.service';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-settings-page',
  templateUrl: './settings-page.component.html',
  styleUrls: ['./settings-page.component.scss']
})
export class SettingsPageComponent implements OnInit {

  constructor(
    private dataService: DataService,
    private http: HttpClient,
    private auth: AuthService
  ) { }

  newBackupForm: FormGroup;

  ngOnInit(): void {
    this.newBackupForm = new FormGroup({
      newBackup: new FormControl(null)
    });
  }


  createBackup() {
    this.http.get("api/Backup/PackToZip").subscribe();

  }
  restoreSite(){
    this.http.get("api/Backup/UnpackFromZip").subscribe();
  }


  // Выбор бэкапа для загрузки на сервер
  selectedFile: File = null;
  onSelectFile(fileInput: any) {
    this.selectedFile = <File>fileInput.target.files[0];
  }

  // Отправка бэкапа на сервер
  uploadBackup(data) {
    const formData = new FormData();
    formData.append('newBackup', this.selectedFile);
    this.http.post('api/backup/loadbackup', formData)
      .subscribe(res => {
        alert('Uploaded!!');
        //this.http.get("api/images/getimages").subscribe((data: string) => this.images = data);
      });
    this.newBackupForm.reset();
  }

  // загрузка бэкапа с сервера на клиент
  public downloadBackup() {
    this.http.get('api/backup/downloadBackup', { responseType: 'blob' }).subscribe(blob => {
      saveAs(blob, 'backup.zip', {
        type: 'application/zip' // --> or whatever you need here
      });
    });
  }
}
