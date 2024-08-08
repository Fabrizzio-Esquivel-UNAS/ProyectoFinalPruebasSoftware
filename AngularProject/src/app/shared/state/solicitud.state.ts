import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, switchMap, tap } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { ISolicitudInfo } from '@domain/solicitud.model';
import { StateBaseList } from '@domain/state.class';

@Injectable({
    providedIn: 'root'
})

export class SolicitudState extends StateBaseList<ISolicitudInfo[]> {

}
