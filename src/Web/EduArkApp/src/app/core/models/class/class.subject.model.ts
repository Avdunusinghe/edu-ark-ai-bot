import { DropDownModel } from './../common/drop.down.model';

export class ClassSubjectModel {
	classId: number;
	subjectId: number;
	subjectName: string;
	subjectTeacherId: number;
	subjectTeachers: DropDownModel[];
}
