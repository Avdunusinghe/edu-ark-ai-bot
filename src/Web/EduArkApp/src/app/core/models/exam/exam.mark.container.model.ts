import { StudentMarksModel } from './student.mark.model';
export class ExamMarkContainerModel {
	subjectId: number;
	examId: number;
	academicLevelId: number;
	studentMarks: StudentMarksModel[] = [];
}
