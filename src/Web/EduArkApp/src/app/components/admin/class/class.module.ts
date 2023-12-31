import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenubarModule } from 'primeng/menubar';
import { ClassRoutingModule } from './class-routing.module';
import { ClassComponent } from './class.component';
import { ClassDetailComponent } from './class-detail/class-detail.component';
import { SharedModule } from '../../shared/shared.module';
import { TabViewModule } from 'primeng/tabview';
import { ClassLayoutComponent } from './class-layout/class-layout.component';
import { ClassStudentsComponent } from './class-students/class-students.component';
import { ClassConfigurationModule } from './../../../modules/class-configuration/class-configuration.module';

@NgModule({
	declarations: [ClassComponent, ClassDetailComponent, ClassLayoutComponent, ClassStudentsComponent],
	imports: [CommonModule, ClassRoutingModule, SharedModule, TabViewModule, MenubarModule, ClassConfigurationModule],
})
export class ClassModule {}
