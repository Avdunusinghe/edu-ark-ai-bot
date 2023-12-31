import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LessonViewComponent } from './lesson-view.component'
import { LessonResourcesComponent } from './lesson-resources/lesson-resources.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'lesson-view',
    pathMatch: 'full',
  },
  {
    path: 'lesson-view',
    component: LessonViewComponent
  },
  {
    path: 'lesson-resources/:id',
    component: LessonResourcesComponent
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LessonViewRoutingModule { }
