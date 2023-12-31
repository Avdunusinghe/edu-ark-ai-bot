import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './core/guard/auth.guard';
import { MainLayoutComponent } from './components/layout/app-layout/main-layout/main-layout.component';
import { AuthLayoutComponent } from './components/layout/app-layout/auth-layout/auth-layout.component';
const routes: Routes = [
	{
		path: '',
		component: MainLayoutComponent,
		canActivate: [AuthGuard],
		children: [
			{ path: '', redirectTo: '/authentication/signin', pathMatch: 'full' },
			{
				path: 'admin',
				loadChildren: () => import('./components/admin/admin.module').then((m) => m.AdminModule),
			},
		],
	},
	{
		path: 'authentication',
		component: AuthLayoutComponent,
		loadChildren: () => import('./components/authentication/authentication.module').then((m) => m.AuthenticationModule),
	},
];
@NgModule({
	imports: [RouterModule.forRoot(routes, {})],

	exports: [RouterModule],
})
export class AppRoutingModule {}
