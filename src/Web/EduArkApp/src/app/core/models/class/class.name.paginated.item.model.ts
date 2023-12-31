import { ClassNameModel } from './class.name.model';
import { PaginatedItemModel } from '../common/paginated.item.model';

export class ClassNamePaginatedItemModel extends PaginatedItemModel {
	items: ClassNameModel[];
}
