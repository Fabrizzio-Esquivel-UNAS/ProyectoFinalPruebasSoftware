import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, switchMap, tap } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';
import { IAuthInfo } from '@domain/auth.model';
import { Router } from '@angular/router';
import { StateBase } from '@domain/state.class';

@Injectable({
    providedIn: 'root'
})
export class AuthState extends StateBase<IAuthInfo> {
    router = inject(Router);
    // redirect update
    get redirectUrl(): string {
        return <string>localStorage.getItem('redirectUrl');
    }
    set redirectUrl(value: string) {
        localStorage.setItem('redirectUrl', value);
    }

    constructor() // to inject the REQUEST token, we do this:
    // @Optional() @Inject(REQUEST) private request: Request
    {
        super();
        // simpler to initiate state here
        // check item validity
        const _localuser: IAuthInfo = this._GetUser()!;

        if (this.checkAuth(_localuser)) {
            this.setState(_localuser);
        } else {
            this.logout(false);
        }
    }
    // localstorage related methods
    private _SaveUser(user: IAuthInfo) {
        localStorage.setItem('user', JSON.stringify(user));
    }
    private _RemoveUser() {
        localStorage.removeItem('user');
    }

    private _GetUser(): IAuthInfo | null {
        // to make it work in SSR, uncomment
        /*
        if (this.request) {
          const _serverCookie = this.request.cookies['CrCookie'];
          if (_serverCookie) {
            try {
              return JSON.parse(_serverCookie);
            } catch (e) {
              // silence
            }
          }
        }
        */
        const _localuser: IAuthInfo = JSON.parse(localStorage.getItem('user') ?? '""');
        if (_localuser && _localuser.accessToken) {
            return <IAuthInfo>_localuser;
        }
        return null;
    }

    // new saveSessions method
    saveSession(user: IAuthInfo): IAuthInfo | null {
        if (user.accessToken) {
            this._SaveUser(user);
            this.setState(user);
            return user;
        } else {
            // remove token from user
            this._RemoveUser();
            this.removeState();
            return null;
        }
    }
    checkAuth(user: IAuthInfo) {
        // if no user, or no accessToken, something terrible must have happened
        if (!user || !user.accessToken) {
            return false;
        }
        // if now is larger that expiresAt, it expired
        //if (Date.now() > user.expiresAt) {
        //    return false;
        //}
        return true;
    }
    // reroute optionally
    logout(reroute: boolean = false) {
        // remove leftover
        this.removeState();
        // and clean localstroage
        this._RemoveUser();
        if (reroute) {
            this.router.navigateByUrl('/login');
        }
    }
    getToken() {
        const _auth = this.stateItem.getValue()!;
        // check if auth is still valid first before you return
        return this.checkAuth(_auth) ? _auth.accessToken : null;
    }
}
