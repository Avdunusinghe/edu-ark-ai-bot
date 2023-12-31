import { PaginatedItemModel } from '../common/paginated.item.model';
import { StudentModel } from './student.model';

export class StudentPaginatedItemModel extends PaginatedItemModel {
	items: StudentModel[];
}
