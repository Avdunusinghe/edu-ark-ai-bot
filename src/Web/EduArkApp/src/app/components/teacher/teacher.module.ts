import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { TeacherRoutingModule } from './teacher-routing.module';
import { TeacherComponent } from './teacher.component';
import { SharedModule } from '../shared/shared.module';
import { UserProfileModule } from '../user-profile/user-profile.module';

@NgModule({
	declarations: [TeacherComponent],
	imports: [CommonModule, TeacherRoutingModule, CardModule, UserProfileModule],
})
export class TeacherModule {}
