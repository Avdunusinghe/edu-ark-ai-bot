import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MyClassesComponent } from './my-classes.component';
import { MyClassesDetailLayoutComponent } from './my-classes-detail-layout/my-classes-detail-layout.component';

const routes: Routes = [
	{
		path: 'my-classes',
		redirectTo: '',
		pathMatch: 'full',
	},
	{
		path: '',
		component: MyClassesComponent,
	},
	{
		path: 'layout/:academicYearId/:academicLevelId/:classNameId/:isClassTeacher',
		component: MyClassesDetailLayoutComponent,
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class MyClassesRoutingModule {}
