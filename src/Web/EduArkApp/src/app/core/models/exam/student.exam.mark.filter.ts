import { CorePaginationFilterModel } from './../common/core.pagination.filter.model';

export class StudentExamMarksFilterModel extends CorePaginationFilterModel {
	studentName: string;
	academicYearId: number;
	academicLevelId: number;
	classNameId: number;
	subjectId: number;
	examType: number;
	semester: number;
	examId: number;
}
