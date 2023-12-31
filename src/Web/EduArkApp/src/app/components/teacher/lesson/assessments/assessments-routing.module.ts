import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AssessmentsComponent } from './assessments.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'assessments',
    pathMatch: 'full',
  },
  {
    path: 'assessments',
    component: AssessmentsComponent,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AssessmentsRoutingModule { }
