import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'learning-plan',
    pathMatch: 'full',
  },
  {
    path: 'learning-plan',
    loadChildren: () => import('./learning-plan/learning-plan.module').then((m) =>m.LearningPlanModule),
  },
  {
    path: 'subjects',
    loadChildren: () => import('./subjects/subjects.module').then((m) => m.SubjectsModule),
  },
  {
    path: 'units',
    loadChildren: () => import('./units/units.module').then((m) => m.UnitsModule),
  },
  {
    path: 'assessments',
    loadChildren: () => import('./assessments/assessments.module').then((m) => m.AssessmentsModule),
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LessonRoutingModule { }
