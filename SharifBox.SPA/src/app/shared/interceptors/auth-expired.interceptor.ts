import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable()
export class AuthExpiredInterceptor implements HttpInterceptor {
  constructor(
    // private loginService: LoginService,
    // private loginModalService: LoginModalService,
    // private stateStorageService: StateStorageService,
    private router: Router
  ) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      tap(() => { }, (err: HttpErrorResponse) => {
        if (err.status === 401 && err.url && !err.url.includes('api/account')) {
          // this.stateStorageService.storeUrl(this.router.routerState.snapshot.url);
          // this.loginService.logout();
          this.router.navigate(['auth']);
          // this.loginModalService.open();
        }
      }));
  }
}
