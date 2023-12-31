export class StudentTargetSettingDetailModel {
	id: number;
	studentId: number;
	studentName: string;
	studentProfileImage: string | null;
	subjectId: number;
	subjectName: string;
	predictedMark: number;
	teacherTaergetScore: number | null;
	grade: string | null;
	severity: string | null;
}
