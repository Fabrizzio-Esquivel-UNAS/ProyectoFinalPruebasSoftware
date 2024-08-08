import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { IFacultadInfo, NewFacultadInfo } from '@domain/facultad.model';
import { StateBase } from '@domain/state.class';
import { FacultadState } from '@state/facultad.state';
import { AutoCompleteCompleteEvent } from 'primeng/autocomplete';
import { first, firstValueFrom, map, switchMap, take } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export abstract class ApiService<T>  {
    protected _apiUrl: string;
    protected _apiState: StateBase<any>;
    http = inject(HttpClient);

    constructor(apiUrl: string, apiState: StateBase<any>){
        this._apiUrl = apiUrl;
        this._apiState = apiState;
    }

    abstract mapResponse(response: ArrayBuffer): any

    mapAllData(itemList: any = this._apiState.getState()) {
        if (itemList==null) return;
        const retData: T[] = itemList.map(
            (itemData: any) => {
                return this.mapResponse(itemData);
            }
        );
        this._apiState.setState(retData);
        return retData;
    };
    getAll(data?: any) {
        return this.http.get(this._apiUrl, data).pipe(
            map((response) => {
                return this.mapAllData((response as any).data.items);
            })
        );
    };
    getById(id: string) {
        return this.http.get(this._apiUrl+"/"+id).pipe(
            map((itemData) => {
                const retData: T = this.mapResponse(itemData as any);
                this._apiState.setState(retData);
                return retData;
            })
        );
    }
    update(data?: any){
        return this.http.put(this._apiUrl, data).pipe(
            map((response) => {
                this._apiState.updateState(data);
                return response;
            })
        );
    }
    add(data?: any){
        return this.http.post(this._apiUrl, data).pipe(
            map((response) => {
                data.id = (response as any).data;
                this._apiState.updateState(data);
                return response;
            })
        );
    }
    delete(id: string){
        return this.http.delete(this._apiUrl+"/"+id).pipe(
            map((response) => {
                this._apiState.removeState(id);
                return response;
            })
        );;
    }
    filter(query: string) {
        let filtered: any[] = [];
        (this._apiState.getState() as any[]).forEach(x => {
            //if (this.filterValidation(x, query)==true) {
            var value = x.fullName || x.nombre;
            if (value.toLowerCase().includes(query.toLowerCase())==true) {
                filtered.push(x);
            }
        })
        return filtered;
    }
}
