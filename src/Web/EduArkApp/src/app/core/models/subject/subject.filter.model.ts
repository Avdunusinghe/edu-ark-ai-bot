import { CorePaginationFilterModel } from './../common/core.pagination.filter.model';

export class SubjectFilterModel extends CorePaginationFilterModel {
	name: string;
	subjectStreamId: number;
}
