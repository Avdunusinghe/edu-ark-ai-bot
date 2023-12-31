export class ResultModel {
	id: number;
	succeeded: boolean;
	successMessage: string;
	data: string;
	errors: Array<string>;
}
