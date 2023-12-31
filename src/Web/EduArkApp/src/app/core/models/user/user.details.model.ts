export class UserDetailsModel {
	id: number;
	firstName: string;
	lastName: string;
	userName: string;
	email: string;
	phoneNumber: string;
	password?: string;
	roles: number[];

	createdUser?: string;
	createdDate?: Date;
	updatedUser?: string;
	updatedDate?: Date;
	isActive?: boolean;
	roleName?: string;
	profileImageUrl?: string;
}
