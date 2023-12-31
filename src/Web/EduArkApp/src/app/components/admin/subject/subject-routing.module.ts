import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SubjectComponent } from './subject.component';

const routes: Routes = [
	{
		path: '',
		redirectTo: 'subjects',
		pathMatch: 'full',
	},
	{
		path: 'subjects',
		component: SubjectComponent,
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class SubjectRoutingModule {}
