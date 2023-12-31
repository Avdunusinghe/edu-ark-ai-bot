export interface ResultModel {
	id: number;
	succeeded: boolean;
	successMessage: string;
	errors: Array<string>;
}
