import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClassNameComponent } from './class-name.component';

const routes: Routes = [
	{
		path: '',
		redirectTo: 'class-name',
		pathMatch: 'full',
	},
	{
		path: 'class-name',
		component: ClassNameComponent,
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class ClassNameRoutingModule {}
