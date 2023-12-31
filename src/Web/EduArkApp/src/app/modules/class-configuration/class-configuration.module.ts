import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClassSettingsComponent } from './class-settings/class-settings.component';
import { ClassStudentUploadComponent } from './class-student-upload/class-student-upload.component';
import { MenubarModule } from 'primeng/menubar';
import { ProgressBarModule } from 'primeng/progressbar';
import { SharedModule } from 'src/app/components/shared/shared.module';
import { ClassDetailFormComponent } from './class-detail-form/class-detail-form.component';
import { ExamMarkSettingComponent } from './exam-mark-setting/exam-mark-setting.component';
@NgModule({
	declarations: [
		ClassSettingsComponent,
		ClassStudentUploadComponent,
		ClassDetailFormComponent,
		ExamMarkSettingComponent,
	],
	imports: [CommonModule, MenubarModule, ProgressBarModule, SharedModule],
	exports: [ClassSettingsComponent, ClassStudentUploadComponent, ClassDetailFormComponent, ExamMarkSettingComponent],
})
export class ClassConfigurationModule {}
