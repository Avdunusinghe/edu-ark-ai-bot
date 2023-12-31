import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UnitsRoutingModule } from './units-routing.module';
import { UnitsComponent } from './units.component';
import { DropdownModule } from 'primeng/dropdown';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { TableModule } from 'primeng/table';
import { CardModule } from 'primeng/card';
import { DialogModule } from 'primeng/dialog';
import { AccordionModule } from 'primeng/accordion';

import { SplitterModule } from 'primeng/splitter';
import { DividerModule } from 'primeng/divider';
import { LessonUnitsAddComponent } from './lesson-units-add/lesson-units-add.component';
import { SharedModule } from 'src/app/components/shared/shared.module';
import { SelectButtonModule } from 'primeng/selectbutton';
import { LessonUnitsUpdateComponent } from './lesson-units-update/lesson-units-update.component';
import { FileUploadModule } from 'primeng/fileupload';
import { TagModule } from 'primeng/tag';

@NgModule({
  declarations: [UnitsComponent, LessonUnitsAddComponent, LessonUnitsUpdateComponent],
  imports: [
    CommonModule,
    UnitsRoutingModule,

    DropdownModule,
    ConfirmDialogModule,
    DialogModule,
    TableModule,
    CardModule,
    AccordionModule,
    SplitterModule,
    DividerModule,
    SharedModule,
    SelectButtonModule,
    FileUploadModule,
    TagModule,
  ]
})
export class UnitsModule { }
