import { PaginatedItemModel } from '../common/paginated.item.model';
import { ClassDetailModel } from './class.detail.model';

export class ClassPaginatedItemModel extends PaginatedItemModel {
	items: ClassDetailModel[];
}
