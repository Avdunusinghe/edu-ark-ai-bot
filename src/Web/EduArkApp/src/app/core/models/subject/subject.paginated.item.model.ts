import { PaginatedItemModel } from '../common/paginated.item.model';
import { SubjectDetailsModel } from './subject.detail.model';

export class SubjectPaginatedItemModel extends PaginatedItemModel {
	items: SubjectDetailsModel[];
}
