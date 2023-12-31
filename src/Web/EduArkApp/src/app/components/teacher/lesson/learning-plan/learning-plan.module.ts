import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LearningPlanRoutingModule } from './learning-plan-routing.module';
import { LearningPlanComponent } from './learning-plan.component';
import { DropdownModule } from 'primeng/dropdown';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { TableModule } from 'primeng/table';


@NgModule({
  declarations: [LearningPlanComponent],
  imports: [
    CommonModule,
    LearningPlanRoutingModule,
    DropdownModule,
    ConfirmDialogModule,
    TableModule,
  ],
})
export class LearningPlanModule { }
