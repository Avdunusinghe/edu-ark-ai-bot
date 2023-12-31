import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TableModule } from 'primeng/table';
import { TenantRoutingModule } from './tenant-routing.module';
import { TenantComponent } from './tenant.component';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TenantDetailComponent } from './tenant-detail/tenant-detail.component';
import { TenantDetailViewComponent } from './tenant-detail-view/tenant-detail-view.component';
import { InputSwitchModule } from 'primeng/inputswitch';
import { TabViewModule } from 'primeng/tabview';
import { TenantDetailFormComponent } from './tenant-detail-form/tenant-detail-form.component';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { TenantDomainValidator } from 'src/app/core/validators/tenant.domain.validator';
@NgModule({
	declarations: [TenantComponent, TenantDetailComponent, TenantDetailViewComponent, TenantDetailFormComponent],
	imports: [
		CommonModule,
		TenantRoutingModule,
		TableModule,
		ConfirmDialogModule,
		DialogModule,
		InputTextModule,
		DropdownModule,
		FormsModule,
		ReactiveFormsModule,
		InputSwitchModule,
		TabViewModule,
		InputTextareaModule,
	],
	providers: [TenantDomainValidator],
})
export class TenantModule {}
