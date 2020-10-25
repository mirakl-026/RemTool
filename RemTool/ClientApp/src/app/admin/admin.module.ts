import { CommonModule } from '@angular/common';
import { NgModule } from "@angular/core"
import { RouterModule } from '@angular/router';

import { AdminLayoutComponent } from './shared/admin-layout/admin-layout.component';

import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';





@NgModule({
  declarations: [
    AdminLayoutComponent,
  ],
  imports: [
    FormsModule,
    HttpClientModule,
    CommonModule,
    RouterModule.forChild([
        {
            path: '', component: AdminLayoutComponent, children: [

            ]
        }
    ])
  ],
  exports: [RouterModule],
})

export class AdminModule {

}
