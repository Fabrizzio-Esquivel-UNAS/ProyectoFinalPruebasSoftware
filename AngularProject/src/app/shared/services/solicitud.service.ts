import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { IEscuelaInfo, NewEscuelaInfo } from '@domain/escuela.model';
import { IFacultadInfo, NewFacultadInfo } from '@domain/facultad.model';
import { ISolicitudInfo, NewSolicitudInfo } from '@domain/solicitud.model';
import { EscuelaState } from '@state/escuela.state';
import { SolicitudState } from '@state/solicitud.state';
import { AutoCompleteCompleteEvent } from 'primeng/autocomplete';
import { first, firstValueFrom, map, take } from 'rxjs';
import { ApiService } from './apiservice.class';
import { UsuarioState } from '@state/usuario.state';
import { TranslocoService } from '@jsverse/transloco';

@Injectable({
  providedIn: 'root'
})
export class SolicitudService extends ApiService<ISolicitudInfo> {
    translocoService = inject(TranslocoService);
    usuarioState = inject(UsuarioState);
    constructor(){
        super('/api/v1/Solicitud', inject(SolicitudState));
        this.getAll().pipe(take(1)).subscribe();
        this.usuarioState.stateItem$.subscribe(_ => {
            this.mapAllData();
        })
    }

    override mapResponse(itemData: any) {
        var solicitud = NewSolicitudInfo(itemData);
        solicitud.asesor = this.usuarioState.getStateItem(solicitud.asesorUserId!);
        solicitud.asesorado = this.usuarioState.getStateItem(solicitud.asesoradoUserId!);

        return solicitud;
    }
}
