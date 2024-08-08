import { Injectable, inject } from '@angular/core';
import {
    ActivatedRouteSnapshot,
    CanActivateFn,
    Route,
    Router,
    RouterStateSnapshot,
    UrlSegment,
} from '@angular/router';
import { map, Observable } from 'rxjs';
import { AuthState } from '@state/auth.state';
import { UserRole } from '@domain/user.model';

@Injectable({ providedIn: 'root' })
class PermissionsService {
    authState = inject(AuthState);
    _router = inject(Router);

    canActivate(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot,
        acceptedRoles?: UserRole[]
    ): Observable<boolean> {
        if (acceptedRoles) {
            return this.secureRole(route, acceptedRoles);
        } else {
            // save snapshop url
            this.authState.redirectUrl = state.url;
            return this.secure(route);
        }
    }

    private secure(route: ActivatedRouteSnapshot | Route): Observable<boolean> {
        // tap into auth state to see if user exists
        return this.authState.stateItem$.pipe(
            map((user) => {
                // if user exists let them in, else redirect to login
                if (!user) {
                    this._router.navigateByUrl('/login');
                    return false;
                }
                // user exists
                return true;
            })
        );
    }

    private secureRole(route: ActivatedRouteSnapshot | Route, acceptedRoles: UserRole[]): Observable<boolean> {
        // tap into auth state to see if user exists
        return this.authState.stateItem$.pipe(
            map((user) => {
                // if user exists and has any accepted role let them in, else redirect to login
                const values = Object.values(acceptedRoles);
                var hasAnyRole = false;
                values.forEach((value) => {
                    const findMe = Object.keys(UserRole)[Object.values(UserRole).indexOf(value)];
                    if (user?.payload?.['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] == findMe) {                            
                        hasAnyRole = true;
                    }
                })
                if (hasAnyRole) {
                    return true;
                }
                this._router.navigateByUrl('/');
                return false;
            })
        );
    }

    // canActivateChild(
    //     route: ActivatedRouteSnapshot,
    //     state: RouterStateSnapshot
    // ): Observable<boolean> {
    //     // save snapshop url
    //     this.authState.redirectUrl = state.url;
    //     return this.canActivateLogin(route, state);
    // }

    // canMatch(
    //     route: Route,
    //     segments: UrlSegment[]
    // ): Observable<boolean> {
    //     // create the current route from segments	  
    //     const fullPath = segments.reduce((path, currentSegment) => {
    //         return `${path}/${currentSegment.path}`;
    //     }, '');

    //     this.authState.redirectUrl = fullPath;

    //     return this.secure(route);
    // }
}

export const AuthGuard: CanActivateFn = (next: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
    return inject(PermissionsService).canActivate(next, state);
}

export function requireAnyRole(...roles: UserRole[]): CanActivateFn {
    return (next: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
        return inject(PermissionsService).canActivate(next, state, roles)
    }
}