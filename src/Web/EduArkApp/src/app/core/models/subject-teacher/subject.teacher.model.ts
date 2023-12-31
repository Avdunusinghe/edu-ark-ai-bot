import { DropDownModel } from './../common/drop.down.model';

export class SubjectTeacherModel {
	id: number;
	academicLevelId: number;
	academicYearId: number;
	subjectId: number;
	subject: string;
	assignedSubjectTeachersName: string;
	assignedTeacherIds: number[];
	assignedTeachersCount: number;
	allTeachers: DropDownModel[];
}
