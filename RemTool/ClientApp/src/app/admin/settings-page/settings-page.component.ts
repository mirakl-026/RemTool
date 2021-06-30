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
  mailSettings$: MailSettings;
  contactsSettings$: ContactsSettings;

  ngOnInit(): void {
    this.mailSettings$ = new MailSettings;
    this.mailSettings$.sendNotificationToHQ = false;
    this.mailSettings$.hQeMail = "";
    this.mailSettings$.sendNotificationToClient = false;
    this.mailSettings$.defaultMessageToClient = "";
    this.mailSettings$.credentials_Name = "";
    this.mailSettings$.credentials_Pass = "";
    this.mailSettings$.smtpServer_Host = "";
    this.mailSettings$.smtpServer_Port = "";
    this.getMailSettings();
    this.newBackupForm = new FormGroup({
      newBackup: new FormControl(null)
    });

    this.contactsSettings$ = new ContactsSettings;
    this.contactsSettings$.phoneNumber = "";
    this.contactsSettings$.email = "";
    this.getContactsSettings();
  }


  createBackup() {
    this.http.get("api/Backup/PackToZip").subscribe();

  }
  restoreSite() {
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
        this.restoreSite();
        alert('База данных восстановлена');
        //this.http.get("api/images/getimages").subscribe((data: string) => this.images = data);
      });
    this.newBackupForm.reset();
  }

  // загрузка бэкапа с сервера на клиент
  public downloadBackup(e) {
    e.target.blur();
    // this.createBackup();
    this.http.get("api/Backup/PackToZip").subscribe(() => {
      this.http.get('api/backup/downloadBackup', { responseType: 'blob' }).subscribe(blob => {
        saveAs(blob, 'backup.zip', {
          type: 'application/zip' // --> or whatever you need here
        });
      });
    });
  }

  // загрузка настроек почты
  getMailSettings() {
    //this.mailSettings = new MailSettings();
    this.http.get("api/RtMailSettings")
      .subscribe((data: MailSettings) => {
        this.mailSettings$ = data;
      });
  }

  // загрузка настроек контактов
  getContactsSettings() {
    //this.mailSettings = new MailSettings();
    this.http.get("api/metadata/getmetadata")
      .subscribe((data: ContactsSettings) => {
        this.contactsSettings$ = data;
      });
  }

  saveMailSettings(e) {
    e.target.blur();
    this.http.put("api/RtMailSettings", this.mailSettings$).subscribe();
    this.http.post("api/metadata/setcontacts", this.contactsSettings$).subscribe();
  }

  resetMailSettings(e) {
    e.target.blur();
    this.mailSettings$.sendNotificationToHQ = false;
    this.mailSettings$.hQeMail = "";
    this.mailSettings$.sendNotificationToClient = false;
    this.mailSettings$.defaultMessageToClient = "";
    this.mailSettings$.credentials_Name = "";
    this.mailSettings$.credentials_Pass = "";
    this.mailSettings$.smtpServer_Host = "";
    this.mailSettings$.smtpServer_Port = "";

    this.http.put("api/RtMailSettings", this.mailSettings$).subscribe();
  }
}

export class MailSettings {

  constructor(
    // флаг об отправке оповещений на почту админа
    public sendNotificationToHQ?: boolean,

    // почта админа для оповещений
    public hQeMail?: string,

    // флаг об отправке на почту клиенту
    public sendNotificationToClient?: boolean,

    // сообщение по умолчанию в письме запросящему
    public defaultMessageToClient?: string,

    // почта за счёт которой идёт отправка
    public credentials_Name?: string,

    public credentials_Pass?: string,

    // SMTP сервер предоставляющий услуги отправки почты
    public smtpServer_Host?: string,

    public smtpServer_Port?: string,
  ) { }
}

export class ContactsSettings {
  constructor(
    public phoneNumber?: string,
    public email?: string
  ) { }
}