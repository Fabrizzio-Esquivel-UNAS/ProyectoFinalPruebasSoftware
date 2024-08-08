import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { IEscuelaInfo, NewEscuelaInfo } from '@domain/escuela.model';
import { IFacultadInfo, NewFacultadInfo } from '@domain/facultad.model';
import { IUserInfo, NewUserInfo, UserRole } from '@domain/user.model';
import { TranslocoService } from '@jsverse/transloco';
import { UsuarioState } from '@state/usuario.state';
import { AutoCompleteCompleteEvent } from 'primeng/autocomplete';
import { first, map, take } from 'rxjs';
import { ApiService } from './apiservice.class';
import { LineaInvestigacionState } from '@state/lineaInvestigacion.state';
import { EscuelaState } from '@state/escuela.state';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService extends ApiService<IUserInfo> {
    translocoService = inject(TranslocoService);
    usuarioState = inject(UsuarioState);
    lineaInvestigacionState = inject(LineaInvestigacionState);
    escuelaState = inject(EscuelaState);

    constructor() {
        super('/api/v1/User', inject(UsuarioState));
        this.getAll().pipe(take(1)).subscribe();
        this.escuelaState.stateItem$.subscribe(_ => {
            this.mapAllData();
        });
        this.lineaInvestigacionState.stateItem$.subscribe(_ => {
            this.mapAllData();
        })
    }
    override mapResponse(itemData: any) {
        var user = NewUserInfo(itemData);
        user.escuela = this.escuelaState.getStateItem(user.escuelaId!);
        user.lineaInvestigacion = this.lineaInvestigacionState.getStateItem(user.lineaInvestigacionId!);
        return user;
    }

    filterAsesor(fullName: string, lineaId?: string) {
        let filtered: any[] = [];
        (this._apiState.getState() as any[]).forEach((x : IUserInfo) => {
            //if (this.filterValidation(x, query)==true) {
            if (x.role == UserRole.Asesor
                && x.fullName!.toLowerCase().includes(fullName.toLowerCase())==true
                && (lineaId==null || x.lineaInvestigacionId!.toLowerCase().includes(lineaId.toLowerCase())==true)) {
                    filtered.push(x);
            }
        })
        return filtered;
    }

    filterCoordinador(fullName: string) {
        let filtered: any[] = [];
        (this._apiState.getState() as any[]).forEach((x : IUserInfo) => {
            //if (this.filterValidation(x, query)==true) {
            var value2 = x.id;
            if (x.role == UserRole.Coordinador
                && x.grupoInvestigacionId==null
                && x.fullName!.toLowerCase().includes(fullName.toLowerCase())==true) {
                    filtered.push(x);
            }
        })
        return filtered;
    }
    
    filterByAnyRole(fullName: string, ...roles: UserRole[]) {
        let filtered: any[] = [];
        (this._apiState.getState() as any[]).forEach(x => {
            //if (this.filterValidation(x, query)==true) {
            var value1 = x.fullName;
            if (value1.toLowerCase().includes(fullName.toLowerCase())==true) {
                if (roles.includes(x.role)) {
                    filtered.push(x);
                }
            }
        })
        return filtered;
    }
}
