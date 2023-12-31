import { CorePaginationFilterModel } from './../common/core.pagination.filter.model';

export class ClassStudentFilterModel extends CorePaginationFilterModel {
	name!: string;
	academicYearId: number;
	academicLevelId: number;
	classNameId: number;
}
