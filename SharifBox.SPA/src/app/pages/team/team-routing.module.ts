import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TeamListComponent } from './team-list/team-list.component';
import { TeamComponent } from './team/team.component';

const routes: Routes = [
  { path: 'list', component: TeamListComponent  },
  { path: '', component: TeamComponent  },
  { path: '/:id', component: TeamComponent  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TeamRoutingModule { }
