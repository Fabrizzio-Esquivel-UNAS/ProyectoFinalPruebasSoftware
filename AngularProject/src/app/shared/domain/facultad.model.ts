export interface IFacultadInfo {
    id?: string;
    nombre?: string;
}

export const NewFacultadInfo = (data: any): IFacultadInfo => {
    return {
        id: data.id,
        nombre: data.nombre,
    };
};
