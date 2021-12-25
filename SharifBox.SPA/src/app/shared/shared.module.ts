

import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InputComponent } from './components/input/input.component';
import { UserCardComponent } from './components/user-card/user-card.component';
import { AreaSvgComponent } from './components/area-svg/area-svg.component';
import { BtnComponent } from './components/btn/btn.component';
import { ReadonlyInputComponent } from './components/readonly-input/readonly-input.component';
import { ReactiveFormsModule } from '@angular/forms';
import { NavslideMenuComponent } from './components/navslide-menu/navslide-menu.component';
import { SignupComponent } from './components/identity/signup/signup.component';
import { VerificationCodeComponent } from './components/identity/verification-code/verification-code.component';
import { loginComponent } from './components/identity/login/login.component';
import { ToastComponent } from './components/toast/toast.component';
import { MpInputDirective } from './directives/mp-input.directive';
import { SwitchCaseDirective } from './directives/switch-case.directive';

import { ParentSvgSecondComponent } from './components/parent-svg-second/parent-svg-second.component';
import { ParentSvgFirstComponent } from './components/parent-svg-first/parent-svg-first.component';
import { ParentContainerSvgComponent } from './components/parent-container-svg/parent-container-svg.component';
import { SecondAreaSvgComponent } from './components/second-area-svg/second-area-svg.component';

@NgModule({
  declarations: [
    NavslideMenuComponent,
    InputComponent,
    UserCardComponent,
    AreaSvgComponent,
    BtnComponent,
    ReadonlyInputComponent,
    ToastComponent,
    SignupComponent,
    loginComponent,
    VerificationCodeComponent,
    MpInputDirective,
SwitchCaseDirective,
ParentSvgSecondComponent,
ParentSvgFirstComponent,
ParentContainerSvgComponent,
SecondAreaSvgComponent,
  ],
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule
  ],
  exports: [InputComponent,
    UserCardComponent,
    AreaSvgComponent,
    BtnComponent,
    ReadonlyInputComponent,
    SignupComponent,
    loginComponent,
    VerificationCodeComponent,
    ToastComponent,
    SwitchCaseDirective,
    ParentSvgSecondComponent,
ParentSvgFirstComponent,
ParentContainerSvgComponent,
SecondAreaSvgComponent
  ],
  entryComponents: [
    ToastComponent
   ]
})
export class SharedModule { }
