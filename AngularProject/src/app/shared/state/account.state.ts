import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, switchMap, tap } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { IAccountInfo, IPreferenciasInfo, NewAccountInfo, NewPreferenciasInfo } from '@domain/account.model';
import { StateBase } from '@domain/state.class';

@Injectable({
    providedIn: 'root'
})
export class AccountState extends StateBase<IAccountInfo> {
    constructor(){
        super();
        // simpler to initiate state here
        const _localpreferencias: IPreferenciasInfo = this._GetPreferencias();
        this.savePreferencias(_localpreferencias)
    }
    // new saveSessions method
    savePreferencias(newPreferencias: IPreferenciasInfo): IPreferenciasInfo {
        const _preferencias = { ...this.stateItem.getValue()?.preferencias, ...newPreferencias };
        this._SavePreferencias(_preferencias);
        this.updateState({
            preferencias: _preferencias
        });
        return _preferencias;
    }
    // localstorage related methods
    private _SavePreferencias(preferencias: IPreferenciasInfo) {
        localStorage.setItem('preferencias', JSON.stringify(preferencias));
    }
    private _RemovePreferencias() {
        localStorage.removeItem('preferencias');
    }
    private _GetPreferencias(): IPreferenciasInfo {
        var preferenciasObject = JSON.parse(localStorage.getItem('preferencias') ?? '""');
        const _localpreferencias: IPreferenciasInfo = NewPreferenciasInfo(preferenciasObject);
        return _localpreferencias;
    }
}
