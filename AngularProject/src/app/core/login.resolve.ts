import { inject, Injectable } from '@angular/core';
import {
    Router,
    ActivatedRouteSnapshot,
    RouterStateSnapshot,
    ResolveFn,
} from '@angular/router';
import { Observable, map } from 'rxjs';
import { AuthState } from '@state/auth.state';

export const LoginResolve: ResolveFn<any> = (
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
): Observable<boolean> => {
    const authState = inject(AuthState);
    const router = inject(Router);

    return authState.stateItem$.pipe(
        map((user) => {
            // if logged in succesfully, go to last url
            if (user) {
                router.navigateByUrl(
                    authState.redirectUrl || '/'
                );
            }
            // does not really matter, I either go in or navigate away
            return true;
        })
    );
}
