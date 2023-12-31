export interface AuthenticationResponseModel {
	isLoginSuccess: number;
	token: string;
	displayName: string;
	userId: number;
	message: string;
	roles: string[];
}
