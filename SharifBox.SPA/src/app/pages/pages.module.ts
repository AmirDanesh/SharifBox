
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PagesRoutingModule } from './pages-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { ProfileComponent } from './profile/profile.component';
import { LayOutComponent } from './layout/layout.component';
import { NgSelectModule } from '@ng-select/ng-select';
import { FormsModule } from '@angular/forms';

import {
  NbSidebarModule,
  NbLayoutModule,
  NbButtonModule,
  NbUserModule,
  NbActionsModule,
  NbTabsetModule,
  NbCardModule,
  NbContextMenuModule,
} from '@nebular/theme';
import { TeamComponent } from './public/team-page/team-page.component';
import { AuthComponent } from './auth/auth.component';
import { SharedModule } from '../shared/shared.module';




@NgModule({
  declarations: [
    AuthComponent,
    ProfileComponent,
    LayOutComponent,
    TeamComponent,
    ],
  imports: [
    CommonModule,
    PagesRoutingModule,
    ReactiveFormsModule,
    NbLayoutModule,
    NbSidebarModule,
    NbButtonModule,
    NbUserModule,
    NbActionsModule,
    NbTabsetModule,
    NbCardModule,
    NbContextMenuModule,
    NgSelectModule,
    FormsModule,
    SharedModule,
  ],
  exports: [
  ]
})
export class PagesModule { }
