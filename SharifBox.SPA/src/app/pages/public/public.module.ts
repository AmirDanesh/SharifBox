import { PagesModule } from './../pages.module';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatStepperModule } from '@angular/material/stepper';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgPersianDatepickerModule } from 'ng-persian-datepicker';
import { SharedModule } from '../../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PublicRoutingModule } from './public-routing.module';
import { AreaReservationComponent } from './area-reservation/area-reservation.component';
import { PaymentComponent } from './payment/payment.component';
import { MatDialogModule } from '@angular/material/dialog';
import { ChangePassComponent } from './change-pass/change-pass.component';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ChildSvgComponent } from './child-svg/child-svg.component';
import { ReservationComponent } from './reservation/reservation.component';
import { Ng2CompleterModule } from 'ng2-completer';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { NgSelectModule } from '@ng-select/ng-select';
import {NgxMaterialTimepickerComponent, NgxMaterialTimepickerModule} from 'ngx-material-timepicker';
@NgModule({
  declarations: [
    AreaReservationComponent,
    PaymentComponent,
    ChangePassComponent,
    ChildSvgComponent,
    ReservationComponent
  ],
  imports: [
    CommonModule,
    PublicRoutingModule,
    SharedModule,
    NgPersianDatepickerModule,
    FormsModule,
    SharedModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatIconModule,
    MatStepperModule,
    MatButtonModule,
    MatFormFieldModule,
    PagesModule,
    Ng2SmartTableModule,
    Ng2CompleterModule,
    NgSelectModule,
    NgxMaterialTimepickerModule
  ],
  entryComponents: [
    NgxMaterialTimepickerComponent
  ]
})
export class PublicModule { }
