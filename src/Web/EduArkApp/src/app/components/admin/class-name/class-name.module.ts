import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TableModule } from 'primeng/table';
import { DropdownModule } from 'primeng/dropdown';
import { ClassNameRoutingModule } from './class-name-routing.module';
import { ClassNameComponent } from './class-name.component';
import { ClassNameDetailComponent } from './class-name-detail/class-name-detail.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DialogModule } from 'primeng/dialog';
import { MultiSelectModule } from 'primeng/multiselect';

@NgModule({
	declarations: [ClassNameComponent, ClassNameDetailComponent],
	imports: [
		CommonModule,
		ClassNameRoutingModule,
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
export class ClassNameModule {}
