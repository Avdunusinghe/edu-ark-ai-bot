import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UnitsComponent } from './units.component';
import { LessonUnitsAddComponent } from './lesson-units-add/lesson-units-add.component';
import { LessonUnitsUpdateComponent } from './lesson-units-update/lesson-units-update.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'units',
    pathMatch: 'full',
  },
  {
    path: 'units',
    component: UnitsComponent
  },
  {
    path: 'lesson-units-add/:id',
    component: LessonUnitsAddComponent,
  },
  // {
  //   path: 'lesson-units-update/:id',
  //   component: LessonUnitsUpdateComponent,
  // },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UnitsRoutingModule { }
