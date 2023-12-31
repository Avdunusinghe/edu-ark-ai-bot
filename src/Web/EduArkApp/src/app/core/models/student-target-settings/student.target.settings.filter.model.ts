import { CorePaginationFilterModel } from './../common/core.pagination.filter.model';
export class StuddentTargetSettingFilterModel extends CorePaginationFilterModel {
	searchText: string;
	classNameId: number;
	academicYearId: number;
	academicLevelId: number;
	subjectId: number;
	semesterId: number;
}
