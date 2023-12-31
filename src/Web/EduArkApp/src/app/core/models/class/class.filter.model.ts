import { CorePaginationFilterModel } from './../common/core.pagination.filter.model';

export class ClassFilterModel extends CorePaginationFilterModel {
	name!: string;
	academiclLevelId: number;
	academicYearId: number;
}
