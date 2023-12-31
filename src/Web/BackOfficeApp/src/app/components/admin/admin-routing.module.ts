import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
	{
		path: '',
		redirectTo: 'tenant',
		pathMatch: 'full',
	},
	{
		path: 'tenant',
		loadChildren: () => import('./tenant/tenant.module').then((m) => m.TenantModule),
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class AdminRoutingModule {}
