import { IUserInfo } from "./user.model";

export interface IGrupoInvestigacionInfo {
    id?: string;
    nombre?: string;
    coordinadorUserId?: string;
    coordinadorUser?: IUserInfo;
}

export const NewGrupoInvestigacionInfo = (data: any): IGrupoInvestigacionInfo => {
    return {
        id: data.id,
        nombre: data.nombre,
        coordinadorUserId: data.coordinadorUserId,
    };
};
