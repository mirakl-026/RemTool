import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-contacts-page',
  templateUrl: './contacts-page.component.html',
  styleUrls: ['./contacts-page.component.scss']
})
export class ContactsPageComponent implements OnInit {

  constructor(
    private http: HttpClient,
  ) { }

  contactsSettings$: ContactsSettings;

  ngOnInit(): void {
    document.title = 'Ремонт строительного инструмента';

    this.http.get("api/metadata/getmetadata")
      .subscribe((data: ContactsSettings) => {
        this.contactsSettings$ = data;
      });


  }

}

class ContactsSettings {
  constructor(
    public phoneNumber?: string,
    public email?: string
  ) {
    phoneNumber = "";
    email = ""
  }
}