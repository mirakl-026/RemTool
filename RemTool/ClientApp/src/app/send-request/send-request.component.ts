import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-send-request',
  templateUrl: './send-request.component.html',
  styleUrls: ['./send-request.component.scss']
})
export class SendRequestComponent implements OnInit {
  requestForm: FormGroup;
  formPopup: boolean;
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
  
  formPopupClose(e){
    console.log(e.path[0]);
    if (e.path[0] == document.querySelector('.request-container')) {
      this.formPopup = false;
      // window.onscroll = function () { };
    } else if (e.target == document.querySelector('.request-form__close')) {
      this.formPopup = false;
    }
  }
  
  sendRequestPopup(e){
    e.target.blur();
    this.formPopup = true;
    // document.querySelector('.request-container').setAttribute('style', `height: ${document.body.clientHeight}px`);
  //   let scrollX = window.scrollX
	// let scrollY = window.scrollY;
  //  window.onscroll = function () { window.scrollTo(scrollX, scrollY); };
  }
  sendRequest(e){
    // var nowDate = new Date();
    // var day = nowDate.getDate();
    // var mounth = nowDate.getMonth();
    // var year = nowDate.getFullYear();
    // var hours = nowDate.getHours();
    // var minutes = nowDate.getMinutes();
    // var time = day + " " + mounth + " " + year + " " + hours + " " + minutes;
    // document.write(time);
    e.target.blur();
    let time = String(Math.trunc(new Date().getTime() / 1000));
    console.log(time);
    var req = {
      "Name": this.requestForm.value.name,
      "Phone": '',
      "Email": this.requestForm.value.email,
      "ReqInfo": this.requestForm.value.text,
      "SendedTime": time
    }
    this.http.post("/api/rtrequest", req).subscribe(res => {
      console.log(res);
    })
  }

  inputFocus(e){
    e.target.previousElementSibling.focus();
  }
}
