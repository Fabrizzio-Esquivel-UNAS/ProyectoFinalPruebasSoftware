import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, switchMap, tap } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { ICitaInfo } from '@domain/cita.model';
import { StateBase, StateBaseList } from '@domain/state.class';

@Injectable({
    providedIn: 'root'
})

export class CitaState extends StateBaseList<ICitaInfo[]> {
}
