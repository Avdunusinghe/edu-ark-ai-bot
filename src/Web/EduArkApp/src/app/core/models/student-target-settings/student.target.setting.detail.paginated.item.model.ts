import { PaginatedItemModel } from './../common/paginated.item.model';
import { StudentTargetSettingDetailModel } from './student.target.setting.detail.model';

export interface StudentTargetSettingDetailPaginatedItemModel extends PaginatedItemModel {
	items: StudentTargetSettingDetailModel[];
}
