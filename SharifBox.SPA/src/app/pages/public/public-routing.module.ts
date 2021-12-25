import { ReservationComponent } from './reservation/reservation.component';
import { ChangePassComponent } from './change-pass/change-pass.component';
import { PaymentComponent } from './payment/payment.component';
import { AreaReservationComponent } from './area-reservation/area-reservation.component';
import { TeamComponent } from './team-page/team-page.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NavslideMenuComponent } from 'src/app/shared/components/navslide-menu/navslide-menu.component';
import { ChildSvgComponent } from './child-svg/child-svg.component';

const routes: Routes = [
  { path: 'team-page', component: TeamComponent },
  { path: 'team-page/:id', component: TeamComponent },
  { path: 'areaReservation', component: AreaReservationComponent },
  { path: 'reservation', component: ReservationComponent },
  { path: 'reservation/:id', component: ReservationComponent },
  { path: 'payment', component: PaymentComponent },
  { path: 'changePass', component: ChangePassComponent },
  { path: 'child', component: ChildSvgComponent },
  { path: '', component: NavslideMenuComponent }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PublicRoutingModule { }
