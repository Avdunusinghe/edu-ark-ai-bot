import { DropDownModel } from './drop.down.model';

export class BaseAcademicMasterDataModel {
	currentAcademicYear: number;
	academicYears: DropDownModel[];
	academicLevels: DropDownModel[];
	semesters: DropDownModel[];
	examTypes: DropDownModel[];
}
