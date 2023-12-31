import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SubjectTeacherComponent } from './subject-teacher.component';
import { SubjectTeacherDetailComponent } from './subject-teacher-detail/subject-teacher-detail.component';

const routes: Routes = [
	{
		path: '',
		redirectTo: 'subject-teacher',
		pathMatch: 'full',
	},
	{
		path: 'subject-teacher',
		component: SubjectTeacherComponent,
	},
	{
		path: 'subject-teacher-detail',
		component: SubjectTeacherDetailComponent,
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class SubjectTeacherRoutingModule {}
