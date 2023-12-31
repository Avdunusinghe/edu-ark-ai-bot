export class TenantDetailModel {
	id: number;
	name: string;
	domain: string;
	databaseServer: string;
	logo!: string;
	serverId!: number;
	connectionString!: string;
	sMTPServer!: string;
	sMTPUsername!: string;
	sMTPPassword!: string;
	sMTPFrom!: string;
	sMTPPort!: number;
	isSMTPUseSSL!: boolean;
	specialNotes!: string;
	isGovernmentSchool: boolean;
}
