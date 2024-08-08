import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, switchMap, tap } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { ICalendarioInfo } from '@domain/calendario.model';
import { StateBase } from '@domain/state.class';

@Injectable({
    providedIn: 'root'
})

export class CalendarioState extends StateBase<ICalendarioInfo> {
}
