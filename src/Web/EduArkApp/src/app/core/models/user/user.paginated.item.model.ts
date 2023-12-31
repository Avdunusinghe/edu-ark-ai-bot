import { PaginatedItemModel } from '../common/paginated.item.model';
import { UserDetailsModel } from './user.details.model';

export class ContractPaginatedItemModel extends PaginatedItemModel {
	items: UserDetailsModel[];
}
