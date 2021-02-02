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
  constructor( ) { }

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
    e.target.blur();
    const reqForm = {
      "Email": this.requestForm.value.email,
      "Name": this.requestForm.value.name,
    }
    
  }

  inputFocus(e){
    e.target.previousElementSibling.focus();
  }
}
