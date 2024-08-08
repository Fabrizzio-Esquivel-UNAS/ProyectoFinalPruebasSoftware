import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { ICitaInfo, NewCitaInfo } from '@domain/cita.model';
import { IFacultadInfo, NewFacultadInfo } from '@domain/facultad.model';
import { CitaState } from '@state/cita.state';
import { AutoCompleteCompleteEvent } from 'primeng/autocomplete';
import { first, firstValueFrom, map, take } from 'rxjs';
import { ApiService } from './apiservice.class';
import { FacultadService } from './facultad.service';
import { UsuarioState } from '@state/usuario.state';

@Injectable({
  providedIn: 'root'
})

export class CitaService extends ApiService<ICitaInfo> {
    usuarioState = inject(UsuarioState);
    constructor() {
        super('/api/v1/Cita', inject(CitaState));
        this.getAll().pipe(take(1)).subscribe();
        this.usuarioState.stateItem$.subscribe(_ => {
            this.mapAllData();
        })        
    }

    override mapResponse(itemData: any) {
        var cita = NewCitaInfo(itemData);
        cita.asesor = this.usuarioState.getStateItem(cita.asesorUserId!);
        cita.asesorado = this.usuarioState.getStateItem(cita.asesoradoUserId!);
        return cita;
    }
}
