import { CorePaginationFilerModel } from './../common/core.pagination.filter.model';

export class TenantFilterModel extends CorePaginationFilerModel {
	name: string;
	tenantTypeStatus: number;
}
