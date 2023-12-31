import { PaginatedItemModel } from './../common/paginated.item.model';
import { AcademicLevelDetailsModel } from './academic.level.details.model';

export class AcademicLevelPaginatedItemModel extends PaginatedItemModel {
	items: AcademicLevelDetailsModel[];
}
