import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
	selector: 'app-teacher',
	templateUrl: './teacher.component.html',
	styleUrls: ['./teacher.component.sass'],
})
export class TeacherComponent {
	/**
	 * Constructor
	 */
	constructor(private _router: Router) {}

	async navigateLessons() {
		await this._router.navigate(['teacher/lesson/units/units']);
	}

	async navigateClasses() {
		await this._router.navigate(['teacher/my-classes']);
	}
}
