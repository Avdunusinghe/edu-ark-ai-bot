import { PaginatedItemModel } from './../common/paginated.item.model';
import { StudentMarksModel } from './student.mark.model';

export class PaginatedStudentExamMarksModel extends PaginatedItemModel {
	items: StudentMarksModel[];
}
