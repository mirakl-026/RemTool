import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ContactsPageComponent } from './contacts-page/contacts-page.component';
import { DeliveryPageComponent } from './delivery-page/delivery-page.component';
import { MainPageComponent } from './main-page/main-page.component';
import { MainLayoutComponent } from './shared/main-layout/main-layout.component';
import { ToolsPageComponent } from './tools-page/tools-page.component';

const routes: Routes = [
  {
    path: '', component: MainLayoutComponent, children: [
      {path: '', redirectTo: '/', pathMatch: 'full'},
      {path: '', component: MainPageComponent},
      {path: 'tools', component: ToolsPageComponent},
      {path: 'delivery', component: DeliveryPageComponent},
      {path: 'contacts', component: ContactsPageComponent}
    ]
  },
  {
    // path: 'admin', loadChildren: './admin/admin.module#AdminModule'
    path: 'admin', loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
