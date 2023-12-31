import { PaginatedItemModel } from '../common/paginated.item.model';
import { TenantDetailModel } from './tenant.detail.model';

export interface TenantPaginatedItemModel extends PaginatedItemModel {
	items: Array<TenantDetailModel>;
}
