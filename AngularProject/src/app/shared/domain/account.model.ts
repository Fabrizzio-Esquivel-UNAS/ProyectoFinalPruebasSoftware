export interface IAccountInfo {
    id?: string;
    asesorId?: string;
    fullName?: string;
    firstName?: string;
    lastName?: string;
    codigo?: string;
    telefono?: string;
    email?: string;
    role?: number;
    preferencias?: IPreferenciasInfo;
}

export interface IPreferenciasInfo {
    lenguaje?: string;
}

export const NewAccountInfo = (data: any): IAccountInfo => {
    const prefData = (data.preferencias == null || data.preferencias == '') ? '{}' : data.preferencias;
    return {
        id: data.id,
        asesorId: data.asesorId,
        fullName: data.firstName+' '+data.lastName,
        firstName: data.firstName,
        lastName: data.lastName,
        codigo: data.codigo,
        telefono: data.telefono,
        email: data.email,
        role: data.role,
        preferencias: NewPreferenciasInfo(JSON.parse(prefData))
    };
};

export const NewPreferenciasInfo = (data: any): IPreferenciasInfo => {
    return {
        lenguaje: data.lenguaje ?? "es"
    }
}
