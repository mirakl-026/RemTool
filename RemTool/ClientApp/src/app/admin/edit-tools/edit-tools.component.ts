import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

export class Dictionary {
  constructor(
    public Keys?: string[],
    public Values?: string[],
  ) { }
}

export class Tool {
  constructor (
    public id?: number,
    public name?: string,
    public type?: string[],
    public brands?: string[],
    public priceList?: Dictionary
  ) { }
}

@Component({
  selector: 'app-edit-tools',
  templateUrl: './edit-tools.component.html',
  styleUrls: ['./edit-tools.component.scss']
})
export class EditToolsComponent implements OnInit {

  
  
  
  // from: FormGroup;
  form: FormGroup;
  
  constructor( ) { }
  
  ngOnInit(): void {
    this.form = new FormGroup({
      name: new FormControl('Шуруповерт'),
      type: new FormControl(null)
    })
    // console.log(name);
  }  

  log(){
    console.log(this.form.value.name);

  }

}
