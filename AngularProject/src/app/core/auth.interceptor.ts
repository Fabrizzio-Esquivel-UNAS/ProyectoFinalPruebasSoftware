import { HttpInterceptorFn } from '@angular/common/http';
import { first, mergeMap } from 'rxjs';
import { environment } from '../../environments/environment';
import { AuthState } from '../shared/state/auth.state';
import { inject } from '@angular/core';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
    if (req.url.startsWith(environment.apiUrl)) {
        const authState = inject(AuthState)
        return authState.stateItem$.pipe(
            mergeMap((currentUser) => {
                if (currentUser) {
                    const authReq = req.clone({
                        setHeaders: {
                            Authorization: `Bearer ${currentUser.accessToken}`
                        },
                    });
                    return next(authReq);
                }
                return next(req);
            })
        );
    }
    return next(req);
};
