export interface PaginatedItemModel {
	pageNumber: number;
	pageSize: number;
	totalCount: number;
	hasPreviousPage: boolean;
	hasNextPage: boolean;
}
