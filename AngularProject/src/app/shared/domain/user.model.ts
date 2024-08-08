import { IEscuelaInfo } from "./escuela.model";
import { ILineaInvestigacionInfo } from "./lineaInvestigacion.model";

export interface IUserInfo {
    id: string;
    firstName?: string;
    lastName?: string;
    fullName?: string;
    codigo?: string;
    telefono?: string;
    email?: string;
    role?: number;
    escuelaId?: string;
    escuela?: IEscuelaInfo;
    lineaInvestigacionId?: string;
    lineaInvestigacion?: ILineaInvestigacionInfo;
    grupoInvestigacionId?: string;
}

export enum UserRole {
    Admin,
    Coordinador,
    Asesor,
    Asesorado,
    User
}

export const NewUserInfo = (data: any): IUserInfo => {
    return {
        id: data.id,
        firstName: data.firstName,
        lastName: data.lastName,
        fullName: data.firstName+' '+data.lastName,
        codigo: data.codigo,
        telefono: data.telefono,
        email: data.email,
        role: data.role,
        escuelaId: data.escuelaId,
        lineaInvestigacionId: data.lineaInvestigacionId,
        grupoInvestigacionId: data.grupoInvestigacionId
    };
};
