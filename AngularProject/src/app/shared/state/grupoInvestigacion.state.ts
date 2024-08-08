import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, switchMap, tap } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { IGrupoInvestigacionInfo } from '@domain/grupoInvestigacion.model';
import { StateBaseList } from '@domain/state.class';

@Injectable({
    providedIn: 'root'
})

export class GrupoInvestigacionState extends StateBaseList<IGrupoInvestigacionInfo[]> {
}
