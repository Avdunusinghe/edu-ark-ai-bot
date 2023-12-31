import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminRoutingModule } from './admin-routing.module';
import { DropdownModule } from 'primeng/dropdown';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AdminComponent } from './admin.component';

@NgModule({
	declarations: [
    AdminComponent
  ],
	imports: [CommonModule, AdminRoutingModule, DropdownModule, FormsModule, ReactiveFormsModule],
})
export class AdminModule {}
