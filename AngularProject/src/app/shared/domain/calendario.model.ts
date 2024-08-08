export interface ICalendarioInfo {
    id?: string;
    schedulingUrl?: string;
}

export const NewCalendarioInfo = (data: any): ICalendarioInfo => {
    return {
        id: data.id,
        schedulingUrl: data.schedulingUrl
    };
};
