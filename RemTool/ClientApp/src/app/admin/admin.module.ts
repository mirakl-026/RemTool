import { CommonModule } from '@angular/common';
import { NgModule } from "@angular/core"
import { RouterModule } from '@angular/router';

import { AdminLayoutComponent } from './shared/admin-layout/admin-layout.component';

import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { SparePartsPageComponent } from '../admin/spareparts-page/spareparts-page.component';
import { ToolTypePageComponent } from '../admin/tooltypes-page/tooltypes-page.component';





@NgModule({
  declarations: [
    AdminLayoutComponent,
    SparePartsPageComponent,
    ToolTypePageComponent,
  ],
  imports: [
    FormsModule,
    HttpClientModule,
    CommonModule,
    RouterModule.forChild([
      {
        path: '', component: AdminLayoutComponent, children: [
          { path: 'spareparts', component: SparePartsPageComponent },
          { path: 'tooltypes', component: ToolTypePageComponent },
        ]
      }
    ])
  ],
  exports: [RouterModule],
})

export class AdminModule {

}
