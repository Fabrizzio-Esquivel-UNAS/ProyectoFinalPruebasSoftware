import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, filter, first, map, take } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
import { IAuthInfo, NewAuthInfo } from '@domain/auth.model';
import { AuthState } from '@state/auth.state';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private _loginUrl = '/api/v1/User/login';
    private _registerUrl = '/api/v1/User';
    private _localUser = '/api/v1/User/me';
    http = inject(HttpClient);
    authState = inject(AuthState);

    signup(data: any) {
        return this.http.post(this._registerUrl, data);
    }

    // login method
    login(data: any) {
        return this.http.post(this._loginUrl, data).pipe(
            filter(x => x!=null),
            take(1),
            map((response) => {
                // prepare the response to be handled, then return
                const retUser: IAuthInfo = NewAuthInfo((<any>response).data);
                console.log(retUser);

                // save session and return user if needed
                return this.authState.saveSession(retUser);
            }),
            catchError((error) => {
                console.log(error);
                return throwError(() => error);
            })
            // if we are setting cookie on server, this is the place to call local server
            //switchMap((user) => this.GetUserData(user))
        );
    }

    logout() {
        this.authState.logout(true);
    }
}
