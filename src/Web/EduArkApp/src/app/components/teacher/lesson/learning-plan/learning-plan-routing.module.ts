import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LearningPlanComponent} from './learning-plan.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'learning-plan',
    pathMatch: 'full',
  },
  {
    path: 'learning-plan',
    component: LearningPlanComponent,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LearningPlanRoutingModule { }
