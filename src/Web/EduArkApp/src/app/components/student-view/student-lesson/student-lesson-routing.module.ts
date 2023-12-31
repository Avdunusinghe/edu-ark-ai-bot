import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'lesson-view',
    pathMatch: 'full',
  },
  {
    path: 'lesson-view',
    loadChildren: () => import('./lesson-view/lesson-view.module').then((m) =>m.LessonViewModule),
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StudentLessonRoutingModule { }
