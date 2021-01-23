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
  mailSettings: MailSettings;

  ngOnInit(): void {
    this.getMailSettings();
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

  // загрузка настроек почты
  getMailSettings() {
    //this.mailSettings = new MailSettings();
    this.http.get("api/RtMailSettings")
      .subscribe((data: MailSettings) => {
        this.mailSettings = data;
      });
  }


  // методы измения настроек почты
  /*
  changeNotificationToHQ() {
    if (this.mailSettings.sendNotificationToHQ == false) {
      this.http.put("api/RtMailSettings/ChangeFlag_notificationToHQ?value=true", {}).subscribe();
      this.getMailSettings();
    }
    else {
      this.http.put("api/RtMailSettings/ChangeFlag_notificationToHQ?value=false", {}).subscribe();
      this.getMailSettings();
    }
  }

  saveHQeMail() {
    this.http.put("api/RtMailSettings/ChangeHQeMail?eMail=" + this.mailSettings.hQeMail, {}).subscribe();
  }

  changeNotificationToClient() {
    if (this.mailSettings.sendNotificationToClient == false) {
      this.http.put("api/RtMailSettings/ChangeFlag_notificationToClient?value=true", {}).subscribe();
      this.getMailSettings();
    }
    else {
      this.http.put("api/RtMailSettings/ChangeFlag_notificationToClient?value=false", {}).subscribe();
      this.getMailSettings();
    }
  }

  saveDefaultMessage() {
    this.http.put("api/RtMailSettings/ChangeDefaultMessageToClient?message=" + this.mailSettings.defaultMessageToClient, {}).subscribe();
  }

  saveCredentialsName() {
    this.http.put("api/RtMailSettings/ChangeCredentials_Name?credentialsName=" + this.mailSettings.credentials_Name, {}).subscribe();
  }

  saveCredentialsPass() {
    this.http.put("api/RtMailSettings/ChangeCredentials_Pass?credentialsPass=" + this.mailSettings.credentials_Pass, {}).subscribe();
  }

  saveSMTP_Host() {
    this.http.put("api/RtMailSettings/ChangeSmtpServer_Host?smtp_host=" + this.mailSettings.smtpServer_Host, {}).subscribe();
  }

  saveSMTP_Pass() {
    this.http.put("api/RtMailSettings/ChangeSmtpServer_Port?smtp_port=" + this.mailSettings.smtpServer_Port, {}).subscribe();
  }
  */

  saveMailSettings() {
    this.http.put("api/RtMailSettings", this.mailSettings).subscribe();
  }

  resetMailSettings() {
    this.mailSettings.sendNotificationToHQ = false;
    this.mailSettings.hQeMail = "";
    this.mailSettings.sendNotificationToClient = false;
    this.mailSettings.defaultMessageToClient = "< h3 > Ваш запрос передан, с Вами свяжутся ...</ h3 >";
    this.mailSettings.credentials_Name = "";
    this.mailSettings.credentials_Pass = "";
    this.mailSettings.smtpServer_Host = "smtp.mail.ru";
    this.mailSettings.smtpServer_Port = "25";

    this.http.put("api/RtMailSettings", this.mailSettings).subscribe();
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
  ){ }
}
