import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { ICalendarioInfo, NewCalendarioInfo } from '@domain/calendario.model';
import { IFacultadInfo, NewFacultadInfo } from '@domain/facultad.model';
import { CalendarioState } from '@state/calendario.state';
import { AutoCompleteCompleteEvent } from 'primeng/autocomplete';
import { catchError, filter, first, firstValueFrom, map, of, switchMap, take, throwError } from 'rxjs';
import { ApiService } from './apiservice.class';
import { FacultadService } from './facultad.service';
import { AccountService } from './account.service';
import { AccountState } from '@state/account.state';
import { Router } from '@angular/router';
import { environment } from '@env/environment';
import { IAccountInfo } from '@domain/account.model';

@Injectable({
  providedIn: 'root'
})

export class CalendarioService extends ApiService<ICalendarioInfo> {
    accountState = inject(AccountState);
    router = inject(Router);
    calendarioUserId: string | undefined;

    constructor() {
        super('/api/v1/Calendario', inject(CalendarioState));
        this.accountState.stateItem$.pipe(
            filter(x => {
                return (x!=null && x.id!=null);
            }),
            switchMap(account => {
                this.calendarioUserId = account!.asesorId ?? account!.id!;
                return this.getByUserId(this.calendarioUserId);                    
            }),
            catchError(errorResponse => {
                const detailedErrors = (errorResponse.error as any).detailedErrors;
                if (detailedErrors && detailedErrors[0].code == "OBJECT_NOT_FOUND") {
                    console.warn('No se ha encontrado un calendario para usuario con Id "'+this.calendarioUserId+'"');
                    return of(errorResponse);
                }
                return throwError(() => errorResponse);
            })
        ).subscribe(calendario => {
        })
    }

    override mapResponse(response: ArrayBuffer) {
        // prepare the response to be handled, then return
        const retData: ICalendarioInfo = NewCalendarioInfo((<any>response).data);
        return retData;
    }
    getByUserId(id: string) {
        return this.getById("user/"+id);
    }
    auth() {
        document.location.href = environment.backendUrl + this._apiUrl + "/auth-calendly";
    }
    link(calendarioId: string, calendarName: string) {
        return this.getById("link/"+calendarioId+"?calendarName="+calendarName);
    }
    sync(calendarioId: string) {
        return this.getById("sync/"+calendarioId);
    }
}
