export class MasterDataUploadResponseModel {
	isSuccess: boolean;
	results: MasterDataFileValidationResultModel[] = [];
}

export class MasterDataFileValidationResultModel {
	isSuccess: boolean;
	validateMessage: string;
}
