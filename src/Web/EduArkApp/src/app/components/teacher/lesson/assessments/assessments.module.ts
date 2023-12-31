import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AssessmentsRoutingModule } from './assessments-routing.module';
import { AssessmentsComponent } from './assessments.component';
import { DropdownModule } from 'primeng/dropdown';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { TableModule } from 'primeng/table';
import { CardModule } from 'primeng/card';


@NgModule({
  declarations: [AssessmentsComponent],
  imports: [
    CommonModule,
    AssessmentsRoutingModule,

    DropdownModule,
    ConfirmDialogModule,
    TableModule,
    CardModule,
  ]
})
export class AssessmentsModule { }
