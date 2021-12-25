import { SharedModule } from '../../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { TeamRoutingModule } from './team-routing.module';
import { Ng2CompleterModule } from "ng2-completer";
import { TeamComponent } from './team/team.component';
import { TeamListComponent } from './team-list/team-list.component';
import { NgSelectModule } from '@ng-select/ng-select';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    TeamComponent,
    TeamListComponent
  ],
  imports: [
    CommonModule,
    TeamRoutingModule,
    Ng2SmartTableModule,
    Ng2CompleterModule,
    FormsModule,
    ReactiveFormsModule,
    NgSelectModule,
    SharedModule
  ]
})
export class TeamModule { }
