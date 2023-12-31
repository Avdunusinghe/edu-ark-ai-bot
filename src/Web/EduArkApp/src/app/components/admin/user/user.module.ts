import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TableModule } from 'primeng/table';
import { TabViewModule } from 'primeng/tabview';
import { UserRoutingModule } from './user-routing.module';
import { UserComponent } from './user.component';
import { DropdownModule } from 'primeng/dropdown';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DialogModule } from 'primeng/dialog';
import { MultiSelectModule } from 'primeng/multiselect';

@NgModule({
	declarations: [UserComponent],
	imports: [
		CommonModule,
		UserRoutingModule,
		TableModule,
		DropdownModule,
		FormsModule,
		ReactiveFormsModule,
		InputTextModule,
		ConfirmDialogModule,
		DialogModule,
		MultiSelectModule,
		TabViewModule,
	],
})
export class UserModule {}
