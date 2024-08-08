import { IFacultadInfo } from "./facultad.model";
import { IGrupoInvestigacionInfo } from "./grupoInvestigacion.model";

export interface ILineaInvestigacionInfo {
    id?: string;
    nombre?: string;
    facultadId?: string;
    grupoInvestigacionId?: string;
    facultad?: IFacultadInfo;
    grupoInvestigacion?: IGrupoInvestigacionInfo;
}

export const NewLineaInvestigacionInfo = (data: any): ILineaInvestigacionInfo => {
    return {
        id: data.id,
        nombre: data.nombre,
        facultadId: data.facultadId,
        grupoInvestigacionId: data.grupoInvestigacionId,
    };
};
