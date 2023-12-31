import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'student-lesson',
    pathMatch: 'full',
  },
  {
    path: 'student-lesson',
    loadChildren: () => import('./student-lesson/student-lesson.module').then((m) =>m.StudentLessonModule),
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StudentViewRoutingModule { }
