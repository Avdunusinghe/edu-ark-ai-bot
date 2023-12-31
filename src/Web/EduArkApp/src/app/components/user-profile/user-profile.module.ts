import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TooltipModule } from 'primeng/tooltip';
import { UserProfileRoutingModule } from './user-profile-routing.module';
import { UserProfileComponent } from './user-profile.component';
import { TabViewModule } from 'primeng/tabview';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { StudentBehaviorFormComponent } from './student-behavior-form/student-behavior-form.component';
import { PasswordResetFormComponent } from './password-reset-form/password-reset-form.component';
import { CheckboxModule } from 'primeng/checkbox';
import { KnobModule } from 'primeng/knob';
import { KeyFilterModule } from 'primeng/keyfilter';
import { RadioButtonModule } from 'primeng/radiobutton';
@NgModule({
	declarations: [UserProfileComponent, StudentBehaviorFormComponent, PasswordResetFormComponent],
	imports: [
		CommonModule,
		UserProfileRoutingModule,
		TabViewModule,
		FormsModule,
		ReactiveFormsModule,
		TooltipModule,
		CheckboxModule,
		KnobModule,
		KeyFilterModule,
		RadioButtonModule,
	],

	exports: [StudentBehaviorFormComponent],
})
export class UserProfileModule {}
