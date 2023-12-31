import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserProfileComponent } from './user-profile.component';

const routes: Routes = [
	{
		path: '',
		redirectTo: 'user-profile',
		pathMatch: 'full',
	},
	{
		path: 'user-profile',
		component: UserProfileComponent,
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class UserProfileRoutingModule {}
