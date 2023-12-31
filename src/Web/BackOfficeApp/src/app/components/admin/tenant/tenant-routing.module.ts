import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TenantComponent } from './tenant.component';
import { TenantDetailComponent } from './tenant-detail/tenant-detail.component';

const routes: Routes = [
	{
		path: '',
		redirectTo: 'tenants',
		pathMatch: 'full',
	},
	{
		path: 'tenants',
		component: TenantComponent,
	},
	{
		path: 'tenant-detail',
		component: TenantDetailComponent,
	},
	{
		path: 'tenant-detail/:id',
		component: TenantDetailComponent,
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class TenantRoutingModule {}
