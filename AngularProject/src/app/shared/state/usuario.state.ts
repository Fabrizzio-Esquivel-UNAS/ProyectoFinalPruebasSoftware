import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, switchMap, tap } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { IAccountInfo, IPreferenciasInfo, NewAccountInfo, NewPreferenciasInfo } from '@domain/account.model';
import { IUserInfo } from '@domain/user.model';
import { StateBaseList } from '@domain/state.class';

@Injectable({
    providedIn: 'root'
})

export class UsuarioState extends StateBaseList<IUserInfo[]> {
}
