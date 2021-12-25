import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanActivateChild, CanLoad, Route, Router, RouterStateSnapshot, UrlSegment, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';

@Injectable({
    providedIn: 'root'
})
export class AuthenticateGuard implements CanActivate, CanActivateChild, CanLoad {

    constructor(private router: Router, private authService: AuthService) {
    }

    canActivate(
        next: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
        // TODO: Check for token expiration
        if (!this.authService.isLoggedIn()) {
            this.router.navigate(['auth'], { queryParams: { returnUrl: state.url } });
            return false;
        }
        return true;
    }
    canActivateChild(
        next: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
        return this.canActivate(next, state);
    }
    canLoad(
        route: Route,
        segments: UrlSegment[]): Observable<boolean> | Promise<boolean> | boolean {
        if (!this.authService.isLoggedIn()) {
            this.router.navigate(['auth']);
            return false;
        }
        return true;
    }
}
