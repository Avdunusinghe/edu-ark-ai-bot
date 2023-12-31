import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SubjectTeacherRoutingModule } from './subject-teacher-routing.module';
import { SubjectTeacherComponent } from './subject-teacher.component';
import { SubjectTeacherDetailComponent } from './subject-teacher-detail/subject-teacher-detail.component';
import { SharedModule } from '../../shared/shared.module';

@NgModule({
	declarations: [SubjectTeacherComponent, SubjectTeacherDetailComponent],
	imports: [CommonModule, SubjectTeacherRoutingModule, SharedModule],
})
export class SubjectTeacherModule {}
