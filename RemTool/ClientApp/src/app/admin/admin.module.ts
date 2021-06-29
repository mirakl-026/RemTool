import { CommonModule } from '@angular/common';
import { NgModule } from "@angular/core"
import { RouterModule } from '@angular/router';
import { LoginPageComponent } from './login-page/login-page.component';
import { AdminLayoutComponent } from './shared/admin-layout/admin-layout.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthGuard } from '../shared/auth.guard';
import { ToolTypePageComponent } from '../admin/tooltypes-page/tooltypes-page.component';
import { SparePartsPageComponent } from '../admin/spareparts-page/spareparts-page.component';
import { QuillModule } from 'ngx-quill';
import { SettingsPageComponent } from './settings-page/settings-page.component';
import { RequestsPageComponent } from './requests-page/requests-page.component';

@NgModule({
    declarations: [
        AdminLayoutComponent,
        LoginPageComponent,
        ToolTypePageComponent,
        SparePartsPageComponent,
        SettingsPageComponent,
        RequestsPageComponent,

    ],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        QuillModule.forRoot(),
        RouterModule.forChild([
            {
                path: '', component: AdminLayoutComponent, children: [
                    { path: '', redirectTo: '/admin/tooltypes', pathMatch: 'full' },
                    { path: 'login', component: LoginPageComponent },
                    // { path: 'spareparts', component: SparePartsPageComponent },
                    // { path: 'tooltypes', component: ToolTypePageComponent },
                    // { path: 'spareparts', component: SparePartsPageComponent, canActivate: [AuthGuard] },
                    { path: 'tooltypes', component: ToolTypePageComponent, canActivate: [AuthGuard] },
                    { path: 'settings', component: SettingsPageComponent },
                    { path: 'requests', component: RequestsPageComponent, canActivate: [AuthGuard] },
                ]
            }
        ])
    ],
    exports: [RouterModule],
})

export class AdminModule {

}
