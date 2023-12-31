import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MyClassesRoutingModule } from './my-classes-routing.module';
import { MyClassesComponent } from './my-classes.component';
import { CardModule } from 'primeng/card';
import { SharedModule } from '../../shared/shared.module';
import { MyClassesDetailLayoutComponent } from './my-classes-detail-layout/my-classes-detail-layout.component';
import { TabViewModule } from 'primeng/tabview';
import { ClassConfigurationModule } from 'src/app/modules/class-configuration/class-configuration.module';
import { StudentSubjectTargetSettingsComponent } from './student-subject-target-settings/student-subject-target-settings.component';
import { ProgressBarModule } from 'primeng/progressbar';
import { StudentExamMarksComponent } from './student-exam-marks/student-exam-marks.component';
import { InputNumberModule } from 'primeng/inputnumber';
import { ClassStudentProfileComponent } from './class-student-profile/class-student-profile.component';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { UserProfileModule } from '../../user-profile/user-profile.module';
import { CheckboxModule } from 'primeng/checkbox';
import { KnobModule } from 'primeng/knob';
import { KeyFilterModule } from 'primeng/keyfilter';
import { RadioButtonModule } from 'primeng/radiobutton';
import { TagModule } from 'primeng/tag';
@NgModule({
	declarations: [
		MyClassesComponent,
		MyClassesDetailLayoutComponent,
		StudentSubjectTargetSettingsComponent,
		StudentExamMarksComponent,
		ClassStudentProfileComponent,
	],
	imports: [
		CommonModule,
		MyClassesRoutingModule,
		CardModule,
		SharedModule,
		TabViewModule,
		ClassConfigurationModule,
		ProgressBarModule,
		InputNumberModule,
		DynamicDialogModule,
		UserProfileModule,
		CheckboxModule,
		KnobModule,
		KeyFilterModule,
		RadioButtonModule,
		TagModule,
	],
})
export class MyClassesModule {}
