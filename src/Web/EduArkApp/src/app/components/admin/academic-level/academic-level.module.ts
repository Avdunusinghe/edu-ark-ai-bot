import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AcademicLevelRoutingModule } from './academic-level-routing.module';
import { AcademicLevelDetailComponent } from './academic-level-detail/academic-level-detail.component';
import { AcademicLevelComponent } from './academic-level.component';
import { TableModule } from 'primeng/table';
import { DropdownModule } from 'primeng/dropdown';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DialogModule } from 'primeng/dialog';
import { MultiSelectModule } from 'primeng/multiselect';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
@NgModule({
	declarations: [AcademicLevelDetailComponent, AcademicLevelComponent],
	imports: [
		CommonModule,
		AcademicLevelRoutingModule,
		TableModule,
		DropdownModule,
		FormsModule,
		ReactiveFormsModule,
		InputTextModule,
		ConfirmDialogModule,
		DialogModule,
		MultiSelectModule,
		DynamicDialogModule
	],
})
export class AcademicLevelModule {}
