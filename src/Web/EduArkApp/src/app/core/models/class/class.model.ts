import { ClassSubjectTeacherModel } from './class.subject.teacher.model';

export class ClassModel {
	academicYearId: number;
	academicLevelId: number;
	classNameId: number;
	name: string;
	classCategoryId: number;
	languageStreamId: number;
	classTeacherId: number;
	classSubjectTeachers: ClassSubjectTeacherModel[];
}
