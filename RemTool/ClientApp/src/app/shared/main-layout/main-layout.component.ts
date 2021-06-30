import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MainTools } from '../resolver.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { utf8Encode } from '@angular/compiler/src/util';
import { url } from 'inspector';
import { ToolType } from 'src/app/DataService/toolType';
// import { PreloaderService } from '../preloader.service';


@Component({
  selector: 'app-main-layout',
  templateUrl: './main-layout.component.html',
  styleUrls: ['./main-layout.component.scss']
})
export class MainLayoutComponent implements OnInit {

  constructor(
    private http: HttpClient,
    private route: ActivatedRoute,
    // private preloaderService: PreloaderService
  ) { }

  searchForm: FormGroup;
  dropMenuFlag: boolean = false;

  sideElectro: boolean = false;
  sideFuel: boolean = false;
  sideGarden: boolean = false;
  sideCompressors: boolean = false;
  sideGenerators: boolean = false;
  sideWelding: boolean = false;
  sideHeatguns: boolean = false;
  sideRest: boolean = false;

  searchMobile: boolean = false;

  tools: MainTools = new MainTools();

  burger: boolean = false;
  searchPlaceholder: string;

  map: boolean = false;

  contactsSettings$: ContactsSettings;

  ngOnInit(): void {
    this.contactsSettings$ = new ContactsSettings;

    this.http.get("api/metadata/getmetadata")
      .subscribe((data: ContactsSettings) => {
        this.contactsSettings$ = data;
      });
    this.searchForm = new FormGroup({
      data: new FormControl(null, [Validators.minLength(2), Validators.maxLength(20)])
    });

    this.route.data.subscribe(data => {
      this.tools = data["res"];
    });

    window.addEventListener('resize', () => {
      if (window.matchMedia("(max-width: 683.98px)").matches) {
        this.searchMobile = true;
      } else {
        this.searchMobile = false;
      }
      if (window.matchMedia("(max-width: 449.98px)").matches) {
        this.searchPlaceholder = 'Найти инструмент';
      } else {
        this.searchPlaceholder = 'Найти инструмент или услугу';
      }
    });

    if (window.matchMedia("(max-width: 683.98px)").matches) {
      this.searchMobile = true;
    } else {
      this.searchMobile = false;
    }

    if (window.matchMedia("(max-width: 449.98px)").matches) {
      this.searchPlaceholder = 'Найти инструмент';
    } else {
      this.searchPlaceholder = 'Найти инструмент или услугу';
    }
  }

  dropMenuHide() {
    this.dropMenuFlag = false;
    this.sideElectro = false;
    this.sideFuel = false;
    this.sideGarden = false;
    this.sideCompressors = false;
    this.sideGenerators = false;
    this.sideWelding = false;
    this.sideHeatguns = false;
    this.sideRest = false;
  }

  arrowDown(e) {
    if (window.matchMedia("(hover: hover)").matches) {
      this.dropMenuFlag = true;
    } else {
      if (e == 't')
        this.dropMenuFlag = !this.dropMenuFlag;
    }
  }

  arrowRight(type) {
    if (window.matchMedia("(hover: hover)").matches) {
      if (type == 'electrom') {
        this.sideElectro = true;
      } else if (type == 'benzom') {
        this.sideFuel = true;
      } else if (type == 'gardenm') {
        this.sideGarden = true;
      } else if (type == 'compressorm') {
        this.sideCompressors = true;
      } else if (type == 'generatorm') {
        this.sideGenerators = true;
      } else if (type == 'weldingm') {
        this.sideWelding = true;
      } else if (type == 'heatgunm') {
        this.sideHeatguns = true;
      } else if (type == 'restm') {
        this.sideRest = true;
      }
    } else {
      if (type == 'electro') {
        this.sideElectro = !this.sideElectro;
      } else if (type == 'benzo') {
        this.sideFuel = !this.sideFuel;
      } else if (type == 'garden') {
        this.sideGarden = !this.sideGarden;
      } else if (type == 'compressor') {
        this.sideCompressors = !this.sideCompressors;
      } else if (type == 'generator') {
        this.sideGenerators = !this.sideGenerators;
      } else if (type == 'welding') {
        this.sideWelding = !this.sideWelding;
      } else if (type == 'heatgun') {
        this.sideHeatguns = !this.sideHeatguns;
      } else if (type == 'rest') {
        this.sideRest = !this.sideRest;
      }
    }
  }
  searchFocuse: boolean;
  resultHover: boolean;
  searchPreloader: boolean;
  mainTypes: string[] = ['electro', 'benzo', 'garden', 'compressor', 'generator', 'welding', 'heatgun', 'rest'];
  searchMainTypes$: any[] = [];
  searchTypes$: string[] = [];
  types = {
    'type': [],
    'category': [],
    'id': []
  }
  searchServices$: string[] = [];
  services = {
    'type': [],
    'category': [],
    'id': []
  }
  searchIds$: string[] = [];

  searchFocuseSet(e) {
    if (this.resultHover) {
      if (e == 'focus') {

      }
    }
  }
  mains: string[] = [];

  // preloader: boolean = this.preloaderService.isLoading();

  searchTool(data) {
    if (!!this.searchForm.value.data) {
      if (this.searchForm.value.data.length > 1) {
        this.searchPreloader = true;
        this.http.get("api/search/find?userInput=" + encodeURI(this.searchForm.value.data)).subscribe(res => {
          this.searchMainTypes$ = [];
          this.types.type = [];
          this.types.category = [];
          this.types.id = [];
          this.services.type = [];
          this.services.category = [];
          this.services.id = [];
          this.searchTypes$ = res["includedTypes"];
          this.searchServices$ = res["includedServices"];
          this.searchIds$ = res["includedIds"];
          this.searchMainTypes$ = res["includedCategories"];
          for (let i = 0; i < this.searchMainTypes$.length; i++) {
            for (let j = 0; j < this.searchMainTypes$[i].length; j++) {
              if (this.searchMainTypes$[i][j]) {
                this.mains.push(this.mainTypes[j]);
                break;
              }
            }
            if (this.searchServices$[i] == '_') {
              this.types.type.push(this.searchTypes$[i]);
              this.types.category.push(this.mains[i]);
              this.types.id.push(this.searchIds$[i]);
            } else {
              this.services.type.push(this.searchTypes$[i]);
              this.services.category.push(this.mains[i]);
              this.services.id.push(this.searchIds$[i]);
            }
          }
          this.searchPreloader = false;
        })
      } else {
        this.searchMainTypes$ = [];
        this.types.type = [];
        this.types.category = [];
        this.types.id = [];
        this.services.type = [];
        this.services.category = [];
        this.services.id = [];
        this.searchTypes$ = [];
        this.searchServices$ = [];
        this.searchIds$ = [];
        this.searchMainTypes$ = [];

      }
    }
  }

  resetSearch() {
    this.searchForm.value.data = '';
    document.getElementById('search').focus();
    // document.querySelector('#search').focus();
  }

  ngAfterViewInit() {
    this.map = true;
  }

  // showPreloader() {
  //   this.preloader = true;
  // }

  // hidePreloader() {
  //   this.preloader = false;
  // }
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