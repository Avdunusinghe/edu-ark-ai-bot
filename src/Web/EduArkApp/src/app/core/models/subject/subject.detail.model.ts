export class SubjectDetailsModel {
	id: number;
	name: string;
	subjectCode: string;
	subjectCategory: number;
	categorysId: number;
	subjectCategoryName: string;
	subjectType: number;
	parentBasketSubjectId: number;
	parentBasketSubjectName: string;
	subjectStreamId: number;
	subjectStreamName: string;
	isActive: boolean;
	createdByName: string;
	createdDate: string;
	updatedByName: string;
	updatedDate: string;
	subjectAcademicLevels: number[];
}
