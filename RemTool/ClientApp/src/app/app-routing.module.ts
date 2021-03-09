import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ResolverService } from './shared/resolver.service';
import { ContactsPageComponent } from './contacts-page/contacts-page.component';
import { DeliveryPageComponent } from './delivery-page/delivery-page.component';
import { MainPageComponent } from './main-page/main-page.component';
import { MainLayoutComponent } from './shared/main-layout/main-layout.component';
import { ToolPageComponent } from './tool-page/tool-page.component';
import { ToolsPageComponent } from './tools-page/tools-page.component';
import { AllToolsPageComponent } from './all-tools-page/all-tools-page.component';
import { FormControlDirective } from '@angular/forms';
import { NotFoundComponent } from './not-found/not-found.component';

const routes: Routes = [
  {
    path: 'admin', loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule)
  },
  {
    path: '',
    component: MainLayoutComponent,
    resolve: {res: ResolverService},
    children: [
      {
        path: '',
        redirectTo: '/',
        pathMatch: 'full',
        // resolve: {res: ResolverService}
      },
      {
        path: '',
        component: MainPageComponent,
        // resolve: {res: ResolverService}
      },
      {
        path: 'tools',
        component: AllToolsPageComponent
      },
      {
        path: 'tools/:type',
        component: ToolsPageComponent,
        resolve: {res: ResolverService}
      },
      {
        path: 'tools/:type/:id',
        component: ToolPageComponent,
        // resolve: { res: ResolverService }
      },
      {
        path: 'delivery',
        component: DeliveryPageComponent
      },
      {
        path: 'contacts',
        component: ContactsPageComponent
      },
      {path: '404', component: NotFoundComponent},
      {path: '**', redirectTo: '/404'}
    ]
  },
  // {
  //   path: 'admin', loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule)
  // },
  // {
  //   path: '**',
  //   component: NotFoundComponent
  // }
];



@NgModule({
  imports: [RouterModule.forRoot(routes, {
    scrollPositionRestoration: 'enabled'
  })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
