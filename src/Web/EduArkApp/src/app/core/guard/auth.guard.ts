import { Injectable } from '@angular/core';
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthenticationService } from '../service/authentication.service';

@Injectable({
	providedIn: 'root',
})
export class AuthGuard {
	constructor(private authService: AuthenticationService, private router: Router) {}

	canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
		if (this.authService.currentUserValue) {
			return true;
		}
		this.router.navigate(['/authentication/signin']);
		return false;
	}
}
