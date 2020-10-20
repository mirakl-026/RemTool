import { CommonModule } from '@angular/common';
import { NgModule } from "@angular/core"
import { RouterModule } from '@angular/router';

import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';


import { AdminLayoutComponent } from './shared/admin-layout/admin-layout.component';
import { ToolsPageComponent } from '../admin/tools-page/tools-page.component';
import { BrandsPageComponent } from '../admin/brands-page/brands-page.component';
import { SparePartsPageComponent } from '../admin/spareparts-page/spareparts-page.component'


@NgModule({
    declarations: [
        AdminLayoutComponent,
        ToolsPageComponent,
        BrandsPageComponent,
        SparePartsPageComponent
    ],
    imports: [
        FormsModule,
        HttpClientModule,
        CommonModule,
        RouterModule.forChild([
            {
                path: '', component: AdminLayoutComponent, children: [
                    {path: 'tools', component: ToolsPageComponent},
                    {path: 'brands', component: BrandsPageComponent},
                    {path: 'spareparts', component: SparePartsPageComponent},
                ]
            }
        ])
    ],
    exports: [RouterModule],
})

export class AdminModule {

}