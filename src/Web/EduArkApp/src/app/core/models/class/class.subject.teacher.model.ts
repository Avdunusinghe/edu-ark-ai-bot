import { DropDownModel } from './../common/drop.down.model';
export class ClassSubjectTeacherModel {
	id: number;
	classNameId: number;
	academicLevelId: number;
	academicYearId: number;
	subjectId: number;
	subjectTeacherId: number;
	subjectName: string;

	allSubjectTeachers: DropDownModel[];
}
