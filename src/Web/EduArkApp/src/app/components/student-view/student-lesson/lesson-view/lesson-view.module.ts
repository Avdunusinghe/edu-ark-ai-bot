import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LessonViewRoutingModule } from './lesson-view-routing.module';
import { CardModule } from 'primeng/card';
import { LessonViewComponent } from './lesson-view.component';
import { PanelModule } from 'primeng/panel';
import { AccordionModule } from 'primeng/accordion';

import { NodeService } from './../../../../core/service/student-view.service';
import { TreeModule } from 'primeng/tree';
import { LessonResourcesComponent } from './lesson-resources/lesson-resources.component';
import { TableModule } from 'primeng/table';
import { DividerModule } from 'primeng/divider';

@NgModule({
  declarations: [ LessonViewComponent, LessonResourcesComponent ],
  imports: [
    CommonModule,
    LessonViewRoutingModule,

    CardModule,
    PanelModule,
    AccordionModule,
    TreeModule,
    TableModule,
    DividerModule,
  ],
  providers: [ NodeService ]
})
export class LessonViewModule { }
