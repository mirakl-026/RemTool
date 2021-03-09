import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { DataService } from '../../DataService/data.service';
import { ToolType } from '../../DataService/toolType';
import { FormControl, FormGroup } from '@angular/forms';
import { AuthService } from 'src/app/shared/auth.service';

@Component({
  selector: 'app-requests-page',
  templateUrl: './requests-page.component.html',
  styleUrls: ['./requests-page.component.scss']
})
export class RequestsPageComponent implements OnInit {

  private urlRtRequests = "api/rtrequest";
  requests: RtRequest[] = [];
  headers = ["id", "name", "phone", "email", "reqInfo", "sendedTime"];
  edit = false;
  editIndex = 0;
  doneArr: boolean[] = [];
  reqText: string[] = [];

  constructor(
    private http: HttpClient
  ) { }

  ngOnInit(): void {
    this.getAllRtRequests();
    
  }

  requestDone(id, i, e) {
    if (e.target.checked) {
      this.requests[i].reqInfo += "<done>";
    } else {
      this.requests[i].reqInfo = this.requests[i].reqInfo.substr(0, this.requests[i].reqInfo.length - 6);
    }
    this.updateRtRequest(i);
  }


  // перейти в режим редактирования заявки
  editModeForReq(i: number) {
    if (this.edit == false) {
      this.edit = true;
      this.editIndex = i;
    } else {
      this.edit = false;
      this.editIndex = 0;
    }

  }

  // получить все заявки
  getAllRtRequests() {
    this.http.get(this.urlRtRequests)
      .subscribe((data: RtRequest[]) => {
        this.requests = data;
        this.doneArr = [];
        this.reqText = [];
        for (let i = 0; i < this.requests.length; i++) {          
          let date = new Date(parseInt(this.requests[i].sendedTime) * 1000);
          this.requests[i].sendedDate = String(date.getDate()) + "." + String(date.getMonth() + 1) + "." + String(date.getFullYear() + "г");
          let hours: string = "";
          let minutes: string = "";
          if (String(date.getHours()).length < 2) {
            hours = "0" + String(date.getHours());
          } else {
            hours = String(date.getHours());
          }
          if (String(date.getMinutes()).length < 2) {
            minutes = "0" + String(date.getMinutes());
          } else {
            minutes = String(date.getMinutes());
          }
          this.requests[i].sendedTime = hours + ":" + minutes;
          if (this.requests[i].reqInfo.substr(this.requests[i].reqInfo.length - 6, this.requests[i].reqInfo.length - 1) == '<done>') {
            this.reqText.push(this.requests[i].reqInfo.substr(0, this.requests[i].reqInfo.length - 6));
            this.doneArr.push(true);
          } else {
            this.reqText.push(this.requests[i].reqInfo);
            this.doneArr.push(false);
          }
        }
        // console.log(this.doneArr);
      });
  }

  // обновить информацю заявки
  updateRtRequest(i: number) {
    // if (this.editIndex == i && this.edit == true) {
      return this.http.put(this.urlRtRequests, this.requests[i]).subscribe((data) => {
        this.getAllRtRequests();

        this.edit = false;
        this.editIndex = 0;
      });
    // }
  }

  // удалить заявку
  deleteRtRequest(i: number) {
    return this.http.delete(this.urlRtRequests + '/' + this.requests[i].id).subscribe((data) => {
        this.getAllRtRequests();
      });
  }

}

export class RtRequest {
  constructor(
    public id? : string, 

    public name? : string,

    public phone? : string,

    public email? : string,

    public reqInfo? : string,

    public sendedTime? : string,

    public sendedDate? : string,
  ) { }
}
