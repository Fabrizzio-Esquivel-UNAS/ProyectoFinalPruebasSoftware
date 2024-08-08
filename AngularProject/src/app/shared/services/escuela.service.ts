import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { IEscuelaInfo, NewEscuelaInfo } from '@domain/escuela.model';
import { IFacultadInfo, NewFacultadInfo } from '@domain/facultad.model';
import { EscuelaState } from '@state/escuela.state';
import { AutoCompleteCompleteEvent } from 'primeng/autocomplete';
import { first, firstValueFrom, map, take } from 'rxjs';
import { ApiService } from './apiservice.class';
import { FacultadService } from './facultad.service';
import { FacultadState } from '@state/facultad.state';

@Injectable({
  providedIn: 'root'
})
export class EscuelaService extends ApiService<IEscuelaInfo> {
    facultadState = inject(FacultadState);
    constructor() {
        super('/api/v1/Escuela', inject(EscuelaState));
        this.getAll().pipe(take(1)).subscribe()
        this.facultadState.stateItem$.subscribe(_ => {
            this.mapAllData();
        })
    }

    override mapResponse(itemData: any) {
        var escuela = NewEscuelaInfo(itemData);
        escuela.facultad = this.facultadState.getStateItem(escuela.facultadId!);
        return escuela;
    }
}
