import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { IFacultadInfo, NewFacultadInfo } from '@domain/facultad.model';
import { FacultadState } from '@state/facultad.state';
import { AutoCompleteCompleteEvent } from 'primeng/autocomplete';
import { first, firstValueFrom, map, switchMap, take } from 'rxjs';
import { ApiService } from './apiservice.class';

@Injectable({
  providedIn: 'root'
})
export class FacultadService extends ApiService<IFacultadInfo> {
    constructor(){
        super('/api/v1/Facultad', inject(FacultadState));
        this.getAll().pipe(take(1)).subscribe();
    }

    override mapResponse(itemData: any) {
        return NewFacultadInfo(itemData);
    }
}
