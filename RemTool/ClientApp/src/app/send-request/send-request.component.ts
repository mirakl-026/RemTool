import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

@Component({
  selector: 'app-send-request',
  templateUrl: './send-request.component.html',
  styleUrls: ['./send-request.component.scss']
})
export class SendRequestComponent implements OnInit {
  requestForm: FormGroup;
  formPopup: boolean;
  preloader: boolean = false;
  sendButtonDisabled: boolean = false;
  thankYouFlag: boolean = false;
  thankYouMessage: string = '';
  response$: any;
  constructor(
    private http: HttpClient
  ) { }

  ngOnInit(): void {
    this.requestForm = new FormGroup({
      email: new FormControl(null, [Validators.required, Validators.email]),
      name: new FormControl(null, []),
      text: new FormControl(null, [Validators.required]),
    })
  }

  formPopupClose(e) {
    if (e.target == document.querySelector('.request-container')) {
      this.thankYouFlag = false;
      this.formPopup = false;
      // window.onscroll = function () { };
    } else if (e.target == document.querySelector('.request-form__close')) {
      this.thankYouFlag = false;
      this.formPopup = false;
    }
  }

  sendRequestPopup(e) {
    e.target.blur();
    this.formPopup = true;
  }

  sendRequest(e) {
    this.preloader = true;
    this.sendButtonDisabled = true;
    e.target.disabled;
    e.target.blur();
    let time = String(Math.trunc(new Date().getTime() / 1000));
    var req = {
      "Name": this.requestForm.value.name,
      "Phone": '',
      "Email": this.requestForm.value.email,
      "ReqInfo": this.requestForm.value.text,
      "SendedTime": time
    }
    this.response$ =  this.http.post("/api/rtrequest", req, {observe: 'response'});
    this.response$
    .pipe(
      catchError(err => {
        if (err.error == 'wait') {
          this.thankYouMessage = "Заявки можно отправлять 1 раз в 3 минуты. Попробуйте позже."
        } else {
          this.thankYouMessage = "Что-то пошло не так, попробуйсте еще раз."
        }
        this.thankYouFlag = true;
        this.preloader = false;
        this.sendButtonDisabled = false;

        return throwError(err);
      })
      )
    .subscribe(res => {
      if (res.status == 200) {
        this.thankYouMessage = "Ваша заявка отправлена и будет обработана в ближайшее время";
        this.requestForm.reset();
      } else if ((res.status == 400) && (res["error"] == "wait")) {
        this.thankYouMessage = "Заявки можно отправлять 1 раз в 3 минуты. Попробуйте позже."
      } else if ((res.status == 400) && (res["error"] == "error")){
        this.thankYouMessage = "Что-то пошло не так, попробуйсте позже."
      } else {
        this.thankYouMessage = "Что-то пошло не так, попробуйсте позже."
      }
      this.preloader = false;
      this.sendButtonDisabled = false;
      this.thankYouFlag = true;
    });
  }

  requestResult(req){
    
  }


  // getUsers(): Observable<User[]> {
  //   return this.http.get('usersP.json').pipe(
  //     map((data) => {
  //       let usersList = data['usersList']
  //       return usersList.map(function (user: any) {
  //         return { name: user.userName, age: user.userAge }
  //       })
  //     }),
  //     catchError((err) => {
  //       return throwError(err)
  //     })
  //   )
  // }



  inputFocus(e) {
    e.target.previousElementSibling.focus();
  }
}
