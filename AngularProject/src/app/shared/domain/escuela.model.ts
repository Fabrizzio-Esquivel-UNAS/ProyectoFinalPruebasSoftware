import { IFacultadInfo, NewFacultadInfo } from "./facultad.model";

export interface IEscuelaInfo {
    id?: string;
    nombre?: string;
    facultadId?: string;
    facultad?: IFacultadInfo;
}

export const NewEscuelaInfo = (data: any): IEscuelaInfo => {
    return {
        id: data.id,
        nombre: data.nombre,
        facultadId: data.facultadId,
    };
};
