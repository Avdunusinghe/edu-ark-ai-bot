import { BootstrapModule } from './bootstrap.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { IconsModule } from './feather-icons.module';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DialogModule } from 'primeng/dialog';
import { MultiSelectModule } from 'primeng/multiselect';
import { TableModule } from 'primeng/table';
import { InputSwitchModule } from 'primeng/inputswitch';
@NgModule({
	declarations: [],
	imports: [
		CommonModule,
		FormsModule,
		ReactiveFormsModule,
		RouterModule,
		IconsModule,
		BootstrapModule,
		TableModule,
		DropdownModule,
		FormsModule,
		ReactiveFormsModule,
		InputTextModule,
		ConfirmDialogModule,
		DialogModule,
		MultiSelectModule,
		InputSwitchModule,
	],
	exports: [
		CommonModule,
		FormsModule,
		ReactiveFormsModule,
		RouterModule,
		IconsModule,
		BootstrapModule,
		TableModule,
		DropdownModule,
		FormsModule,
		ReactiveFormsModule,
		InputTextModule,
		ConfirmDialogModule,
		DialogModule,
		MultiSelectModule,
		InputSwitchModule,
	],
})
export class SharedModule {}
