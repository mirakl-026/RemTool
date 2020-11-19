import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainLayoutComponent } from './shared/main-layout/main-layout.component';
import { MainPageComponent } from './main-page/main-page.component';
import { ToolsPageComponent } from './tools-page/tools-page.component';
import { ContactsPageComponent } from './contacts-page/contacts-page.component';
import { SwiperComponent } from './swiper/swiper.component';
import { SwiperBrandsComponent } from './swiper-brands/swiper-brands.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { PopupComponent } from './popup/popup.component';
import { QuillModule } from 'ngx-quill';
import { AuthInterceptor } from './shared/auth.interceptor';
import { ToolPageComponent } from './tool-page/tool-page.component';
import { PreloaderComponent } from './preloader/preloader.component';
import { DataService } from './DataService/data.service';

@NgModule({
  declarations: [
    AppComponent,
    MainLayoutComponent,
    MainPageComponent,
    ToolsPageComponent,
    ContactsPageComponent,
    SwiperComponent,
    SwiperBrandsComponent,
    PopupComponent,
    ToolPageComponent,
    PreloaderComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    QuillModule.forRoot()
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      multi: true,
      useClass: AuthInterceptor
    },
    DataService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
