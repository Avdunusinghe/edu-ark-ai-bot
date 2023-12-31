import { PaginatedItemModel } from '../common/paginated.item.model';
import { BasicTeacherClassModel } from './basic.teacher.class.model';

export class BasicTeacherClassPaginatedItemModel extends PaginatedItemModel {
	items: BasicTeacherClassModel[];
}
