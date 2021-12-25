import { UserPageComponent } from './user/user-page/user-page.component';
import { UserListComponent } from './user/user-list/user-list.component';
import { NewsListComponent } from './news-list/news-list.component';
import { NewsComponent } from './news/news.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ContainerSvgComponent } from './container-svg/container-svg.component';

const routes: Routes = [
  { path: 'news-list', component: NewsListComponent  },
  { path: 'news', component: NewsComponent  },
  { path: 'news/:id', component: NewsComponent  },
  { path: 'area', component: ContainerSvgComponent  },
  {path:'users-list', component: UserListComponent},
  {path:'user-page/:id', component: UserPageComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
