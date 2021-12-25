import { MatSnackBarModule, MAT_SNACK_BAR_DEFAULT_OPTIONS } from '@angular/material/snack-bar';
import { PagesModule } from './pages/pages.module';
import { AppRoutingModule } from './app-routing.module';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import {
  DEFAULT_MEDIA_BREAKPOINTS,
  DEFAULT_THEME,
  NbLayoutDirection,
  NbMenuModule,
  NbSidebarModule,
  NbThemeModule,
} from '@nebular/theme';
import { DARK_THEME } from '@nebular/theme';
import { NbEvaIconsModule } from '@nebular/eva-icons';
import { MpInputDirective } from './shared/directives/mp-input.directive';
import { NgxWebstorageModule } from 'ngx-webstorage';
import { AuthInterceptor } from './shared/interceptors/auth.interceptor';
import { AuthExpiredInterceptor } from './shared/interceptors/auth-expired.interceptor';
import { ErrorHandlerInterceptor } from './shared/interceptors/errorhandler.interceptor';
import { NotificationInterceptor } from './shared/interceptors/notification.interceptor';
import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';
import { SwitchCaseDirective } from './shared/directives/switch-case.directive';

declare module '@angular/core' {
  interface ModuleWithProviders<T = any> {
    ngModule: Type<T>;
    providers?: Provider[];
  }
}
@NgModule({
  declarations: [
    AppComponent,

  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    RouterModule,
    AppRoutingModule,
    HttpClientModule,
    PagesModule,
    NbEvaIconsModule,
    MatSnackBarModule,
    NgxMaterialTimepickerModule,
    NgxWebstorageModule.forRoot({ prefix: 'box', separator: '-', caseSensitive: true }),
    NbSidebarModule.forRoot(),
    NbMenuModule.forRoot(),
    NbThemeModule.forRoot(
      { name: window.matchMedia('(prefers-color-scheme: light)').matches ? 'default' : 'dark' },
      [DARK_THEME, DEFAULT_THEME],
      DEFAULT_MEDIA_BREAKPOINTS,
      NbLayoutDirection.RTL)
  ],
  providers: [
    {
      provide: MAT_SNACK_BAR_DEFAULT_OPTIONS,
      useValue: { duration: 3000 }
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthExpiredInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorHandlerInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: NotificationInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
