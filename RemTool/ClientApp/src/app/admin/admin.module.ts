import { CommonModule } from '@angular/common';
import { NgModule } from "@angular/core"
import { RouterModule } from '@angular/router';
import { LoginPageComponent } from './login-page/login-page.component';
import { AdminLayoutComponent } from './shared/admin-layout/admin-layout.component';
import { DashboardPageComponent } from './dashboard-page/dashboard-page.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EditToolsComponent } from './edit-tools/edit-tools.component';

@NgModule({
    declarations: [
        AdminLayoutComponent,
        LoginPageComponent,
        DashboardPageComponent,
        EditToolsComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forChild([
            {
                path: '', component: AdminLayoutComponent, children: [
                    {path: '', redirectTo: '/admin/login', pathMatch: 'full'},
                    {path: 'login', component: LoginPageComponent},
                    {path: 'dashboard', component: DashboardPageComponent},
                    {path: 'edit-tools', component: EditToolsComponent},
                ]
            }
        ])
    ],
    exports: [RouterModule],
})

export class AdminModule {

}