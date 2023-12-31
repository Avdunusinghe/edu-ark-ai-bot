export interface AuthenticationResponseModel {
	isLoginSuccess: number;
	token: string;
	tenantDomain: string;
	displayName: string;
	routePath: string;
	userId: number;
	message: string;
	roles: string[];
	profileImageUrl: string;
}
