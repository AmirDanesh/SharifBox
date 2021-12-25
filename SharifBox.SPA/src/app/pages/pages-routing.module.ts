
import { ProfileComponent } from './profile/profile.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayOutComponent } from './layout/layout.component';
import { AuthComponent } from './auth/auth.component';

const routes: Routes = [
  { path: 'auth', component: AuthComponent },
  {
    path: '', component: LayOutComponent, children: [
      { path: 'profile', component: ProfileComponent },
      { path: 'profile/:id', component: ProfileComponent },
      { path: 'admin', loadChildren: () => import('./admin/admin.module').then(x => x.AdminModule) },
      { path: 'team', loadChildren: () => import('./team/team.module').then(x => x.TeamModule) },
      { path: '', loadChildren: () => import('./public/public.module').then(x => x.PublicModule) },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PagesRoutingModule { }
