import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TeacherComponent } from './teacher.component';

const routes: Routes = [
	{
		path: '',
		redirectTo: 'teacher',
		pathMatch: 'full',
	},
	{
		path: 'teacher',
		component: TeacherComponent,
	},
	{
		path: 'lesson',
		loadChildren: () => import('./lesson/lesson.module').then((m) => m.LessonModule),
	},
	{
		path: 'my-classes',
		loadChildren: () => import('./my-classes/my-classes.module').then((m) => m.MyClassesModule),
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class TeacherRoutingModule {}
