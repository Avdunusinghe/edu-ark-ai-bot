import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AcademicLevelComponent } from './academic-level.component';

const routes: Routes = [
	{
		path: '',
		redirectTo: 'academic-levels',
		pathMatch: 'full',
	},
	{
		path: 'academic-levels',
		component: AcademicLevelComponent,
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class AcademicLevelRoutingModule {}
