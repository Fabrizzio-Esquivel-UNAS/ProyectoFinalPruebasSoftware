import { jwtDecode } from "jwt-decode";

// auth mode is what comes back after login
export interface IAuthInfo {
    accessToken?: string;
    payload?: IPayloadInfo;
}

export interface IPayloadInfo {
    ['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']: string;
    ['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress']: string;
    ['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']: string;
    ['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']: string;
}

// example returnd from server
// {
// 	accessToken: 'access_token',
// 	refreshToken: "refres_token",
// 	payload: {
// 		name: 'maybe name',
// 		id: 'id',
// 		email: 'username'
// 	},
// 	// expires in is an absolute lifetime in seconds
// 	expiresIn: 3600
// }

export const NewAuthInfo = (data: any): IAuthInfo => {
    const jwtPayload = jwtDecode<IPayloadInfo>(data.accessToken);
    return {
        accessToken: data.accessToken,
        payload: jwtPayload
    };
};
