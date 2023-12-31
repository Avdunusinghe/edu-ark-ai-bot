import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SubjectTargetSettingComponent } from './subject-target-setting.component';

const routes: Routes = [
	{
		path: 'subject-target-setting',
		redirectTo: '',
		pathMatch: 'full',
	},
	{
		path: '',
		component: SubjectTargetSettingComponent,
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class SubjectTargetSettingRoutingModule {}
