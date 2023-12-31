import { CorePaginationFilterModel } from './../common/core.pagination.filter.model';

export class TeacherClassFilterModel extends CorePaginationFilterModel {
	name: string;
	academicLevelId: number;
	academicYearId: number;
	classNameId: number;
	classCategoryId: number;
	languageStreamId: number;
	subJectId?: number;
	showMySubjectClasses: boolean;
}
