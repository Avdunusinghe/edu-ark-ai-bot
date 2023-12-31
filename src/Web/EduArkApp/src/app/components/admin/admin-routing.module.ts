import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './admin.component';

const routes: Routes = [
	{
		path: '',
		redirectTo: 'admin',
		pathMatch: 'full',
	},
	{
		path: 'admin',
		component: AdminComponent,
	},
	{
		path: 'user',
		loadChildren: () => import('./user/user.module').then((m) => m.UserModule),
	},
	{
		path: '',
		redirectTo: 'academic-level',
		pathMatch: 'full',
	},
	{
		path: 'academic-level',
		loadChildren: () => import('./academic-level/academic-level.module').then((m) => m.AcademicLevelModule),
	},
	{
		path: '',
		redirectTo: 'subject',
		pathMatch: 'full',
	},
	{
		path: 'subject',
		loadChildren: () => import('./subject/subject.module').then((m) => m.SubjectModule),
	},
	{
		path: '',
		redirectTo: 'class-name',
		pathMatch: 'full',
	},
	{
		path: 'class-name',
		loadChildren: () => import('./class-name/class-name.module').then((m) => m.ClassNameModule),
	},
	{
		path: '',
		redirectTo: 'class',
		pathMatch: 'full',
	},
	{
		path: 'class',
		loadChildren: () => import('./class/class.module').then((m) => m.ClassModule),
	},
	{
		path: '',
		redirectTo: 'subject-teacher',
		pathMatch: 'full',
	},
	{
		path: 'subject-teacher',
		loadChildren: () => import('./subject-teacher/subject-teacher.module').then((m) => m.SubjectTeacherModule),
	},
	{
		path: 'subject-target-setting',
		loadChildren: () =>
			import('./subject-target-setting/subject-target-setting.module').then((m) => m.SubjectTargetSettingModule),
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class AdminRoutingModule {}
