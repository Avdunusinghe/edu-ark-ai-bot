import { CorePaginationFilterModel } from './../common/core.pagination.filter.model';

export class UserDetailsFilterModel extends CorePaginationFilterModel {
	name?: string;
	selectedRole?: number;
	userActiveStatus?: number;
}
