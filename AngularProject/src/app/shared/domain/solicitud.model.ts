import { IUserInfo } from "./user.model";

export interface ISolicitudInfo {
    id?: string,
    asesoradoUserId?: string,
    asesorUserId?: string,
    numeroTesis?: number,
    estado?: number,
    fechaCreacion?: string,
    mensaje?: string,
    asesorado?: IUserInfo,
    asesor?: IUserInfo,
}

export enum SolicitudStatus
{
    Pendiente,
    Aceptado,
    Rechazado
}

export const NewSolicitudInfo = (data: any): ISolicitudInfo => {
    return {
        id: data.id,
        asesoradoUserId: data.asesoradoUserId,
        asesorUserId: data.asesorUserId,
        numeroTesis: data.numeroTesis,
        estado: data.estado,
        fechaCreacion: data.fechaCreacion,
        mensaje: data.mensaje
    };
};
