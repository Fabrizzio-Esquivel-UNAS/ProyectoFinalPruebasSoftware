import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, switchMap, tap } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { IEscuelaInfo } from '@domain/escuela.model';
import { StateBaseList } from '@domain/state.class';

@Injectable({
    providedIn: 'root'
})

export class EscuelaState extends StateBaseList<IEscuelaInfo[]> {
}
