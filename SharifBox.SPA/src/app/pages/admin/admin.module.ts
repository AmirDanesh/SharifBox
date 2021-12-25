import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminRoutingModule } from './admin-routing.module';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { NgPersianDatepickerModule } from 'ng-persian-datepicker';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { Ng2CompleterModule } from "ng2-completer";
import { NewsComponent } from './news/news.component';
import { NewsListComponent } from './news-list/news-list.component';
import { SidebarSvgComponent } from './sidebar-svg/sidebar-svg.component';
import { ContainerSvgComponent } from './container-svg/container-svg.component';
import { NgSelectModule } from '@ng-select/ng-select';
import { SharedModule } from 'src/app/shared/shared.module';
import { UserListComponent } from './user/user-list/user-list.component';
import { UserPageComponent } from './user/user-page/user-page.component';

@NgModule({
  declarations: [NewsComponent,
    NewsListComponent,
    SidebarSvgComponent,
    ContainerSvgComponent,
    UserListComponent,
    UserPageComponent],
  imports: [
    CommonModule,
    AdminRoutingModule,
    CKEditorModule,
    NgPersianDatepickerModule,
    ReactiveFormsModule,
    Ng2SmartTableModule,
    Ng2CompleterModule,
    FormsModule,
    SharedModule,
    NgSelectModule,

  ]
})
export class AdminModule { }
