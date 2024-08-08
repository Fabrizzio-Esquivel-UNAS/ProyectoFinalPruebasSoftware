import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { IGrupoInvestigacionInfo, NewGrupoInvestigacionInfo } from '@domain/grupoInvestigacion.model';
import { GrupoInvestigacionState } from '@state/grupoInvestigacion.state';
import { AutoCompleteCompleteEvent } from 'primeng/autocomplete';
import { first, firstValueFrom, map, switchMap, take } from 'rxjs';
import { ApiService } from './apiservice.class';
import { UsuarioState } from '@state/usuario.state';

@Injectable({
  providedIn: 'root'
})
export class GrupoInvestigacionService extends ApiService<IGrupoInvestigacionInfo> {
    usuarioState = inject(UsuarioState);
    constructor(){
        super('/api/v1/GrupoInvestigacion', inject(GrupoInvestigacionState));
        this.getAll().pipe(take(1)).subscribe();
        this.usuarioState.stateItem$.subscribe(_ => {
            this.mapAllData();
        })
    }

    override mapResponse(itemData: any) {
        var grupo = NewGrupoInvestigacionInfo(itemData);
        if (grupo.coordinadorUserId) {
            grupo.coordinadorUser = this.usuarioState.getStateItem(grupo.coordinadorUserId!);
        }
        return grupo;
    }
}
