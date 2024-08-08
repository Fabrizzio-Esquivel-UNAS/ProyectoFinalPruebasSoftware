import { IUserInfo } from "./user.model";

export interface ICitaInfo {
    id?: string;
    asesorUserId?: string;
    asesor?: IUserInfo;
    asesoradoUserId?: string;
    asesorado?: IUserInfo;
    desarrolloAsesor?: string;
    desarrolloAsesorado?: string;
    fechaCreacion?: string;
    fechaInicio?: string;
    fechaFin?: string;
    estado?: CitaEstado;
}

export enum CitaEstado {
    Programado,
    Completado,
    Inasistido,
    Justificado,
    Cancelado   
}

export const NewCitaInfo = (data: any): ICitaInfo => {
    return {
        id: data.id,
        asesorUserId: data.asesorUserId,
        asesoradoUserId: data.asesoradoUserId,
        desarrolloAsesor: data.desarrolloAsesor ?? "",
        desarrolloAsesorado: data.desarrolloAsesorado ?? "",
        estado: data.estado,
        fechaCreacion: data.fechaCreacion,
        fechaInicio: data.fechaInicio,
        fechaFin: data.fechaFin
    };
};
