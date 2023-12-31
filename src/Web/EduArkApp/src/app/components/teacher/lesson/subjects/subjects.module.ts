import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SubjectsRoutingModule } from './subjects-routing.module';
import { SubjectsComponent } from './subjects.component';
import { TableModule } from 'primeng/table';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DropdownModule } from 'primeng/dropdown';

@NgModule({
  declarations: [SubjectsComponent],
  imports: [
    CommonModule,
    SubjectsRoutingModule,
    DropdownModule,
    ConfirmDialogModule,
    TableModule,
  ]
})
export class SubjectsModule { }
