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

@NgModule({
  declarations: [
    AppComponent,
    MainLayoutComponent,
    MainPageComponent,
    ToolsPageComponent,
    ContactsPageComponent,
    SwiperComponent,
    SwiperBrandsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
