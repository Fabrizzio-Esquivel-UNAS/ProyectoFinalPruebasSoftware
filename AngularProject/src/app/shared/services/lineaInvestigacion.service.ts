import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { ILineaInvestigacionInfo, NewLineaInvestigacionInfo } from '@domain/lineaInvestigacion.model';
import { LineaInvestigacionState } from '@state/lineaInvestigacion.state';
import { AutoCompleteCompleteEvent } from 'primeng/autocomplete';
import { first, firstValueFrom, map, switchMap, take } from 'rxjs';
import { ApiService } from './apiservice.class';
import { FacultadState } from '@state/facultad.state';
import { GrupoInvestigacionState } from '@state/grupoInvestigacion.state';

@Injectable({
  providedIn: 'root'
})
export class LineaInvestigacionService extends ApiService<ILineaInvestigacionInfo> {
    facultadState = inject(FacultadState);
    grupoInvestigacionState = inject(GrupoInvestigacionState);
    constructor(){
        super('/api/v1/LineaInvestigacion', inject(LineaInvestigacionState));
        this.getAll().pipe(take(1)).subscribe();
        this.facultadState.stateItem$.subscribe(_ => {
            this.mapAllData();
        })
        this.grupoInvestigacionState.stateItem$.subscribe(_ => {
            this.mapAllData();
        })
    }

    override mapResponse(itemData: any) {
        var lineaInvestigacion = NewLineaInvestigacionInfo(itemData);
        lineaInvestigacion.facultad = this.facultadState.getStateItem(lineaInvestigacion.facultadId!);
        lineaInvestigacion.grupoInvestigacion = this.grupoInvestigacionState.getStateItem(lineaInvestigacion.grupoInvestigacionId!);
        return lineaInvestigacion;
    }
}
