import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClassComponent } from './class.component';
import { ClassDetailComponent } from './class-detail/class-detail.component';

const routes: Routes = [
	{
		path: '',
		redirectTo: 'class',
		pathMatch: 'full',
	},
	{
		path: 'class',
		component: ClassComponent,
	},
	{
		path: 'class-detail',
		component: ClassDetailComponent,
	},
	{
		path: 'class-detail/:academicYearId/:academicLevelId/:classNameId',
		component: ClassDetailComponent,
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class ClassRoutingModule {}
