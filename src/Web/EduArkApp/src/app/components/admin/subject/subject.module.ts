import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SubjectRoutingModule } from './subject-routing.module';
import { SubjectComponent } from './subject.component';
import { SubjectDetailComponent } from './subject-detail/subject-detail.component';
import { TableModule } from 'primeng/table';
import { DropdownModule } from 'primeng/dropdown';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DialogModule } from 'primeng/dialog';
import { MultiSelectModule } from 'primeng/multiselect';

@NgModule({
	declarations: [SubjectComponent, SubjectDetailComponent],
	imports: [
		CommonModule,
		SubjectRoutingModule,
		TableModule,
		DropdownModule,
		FormsModule,
		ReactiveFormsModule,
		InputTextModule,
		ConfirmDialogModule,
		DialogModule,
		MultiSelectModule,
	],
})
export class SubjectModule {}
