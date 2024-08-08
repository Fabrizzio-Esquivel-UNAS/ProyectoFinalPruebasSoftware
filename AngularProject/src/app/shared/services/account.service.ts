import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { catchError, filter, map, Observable, of, throwError } from 'rxjs';
import { IAccountInfo, IPreferenciasInfo, NewAccountInfo } from '@domain/account.model';
import { IAuthInfo } from '@domain/auth.model';
import { TranslocoService } from '@jsverse/transloco';
import { AccountState } from '../state/account.state';
import { AuthService } from './auth.service';

// account model with one property, whether user is new across devices

@Injectable({ providedIn: 'root' })
export class AccountService {
    private _preferenciasUrl = '/api/v1/User/changePreferencias';
    private _accountUrl = '/api/v1/User/me';
    _http = inject(HttpClient);
    translocoService = inject(TranslocoService);
    accountState = inject(AccountState);
    authService = inject(AuthService);

    getAccount(): Observable<IAccountInfo> {
        // a real url would be /account
        return this._http.get(this._accountUrl).pipe(
            catchError((response : HttpErrorResponse) => {
                console.warn("Couldn't get info from account! Logging out..");
                this.authService.logout();                
                return of(response);
            }),
            map((response) => {
                if (response && (response as any).data) {
                    // update set account state
                    const retAccount: IAccountInfo = NewAccountInfo((response as any).data);
                    this.accountState.setState(retAccount);
                    console.log('Account info received: ', retAccount);
                    return retAccount;
                }else{
                    this.accountState.removeState();
                    return response;
                }
            })
        );
    }

    setLenguaje(_lang: string) {
        const lang = _lang.toLowerCase();
        this.translocoService.setActiveLang(lang);
        const newPreferencias = this.accountState.savePreferencias({
            lenguaje: lang
        });
        this.savePreferencias(newPreferencias);
    }

    savePreferencias(data: IPreferenciasInfo) {
        return this._http.post(this._preferenciasUrl, JSON.stringify(data)).pipe(
            map((response) => {
                return response;
            })
        );
    }

    // also a SaveAccount to set newUser flag to false
    // would leave that to you
}
