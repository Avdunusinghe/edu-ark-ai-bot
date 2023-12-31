import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService, TreeNode } from 'primeng/api';
import { LessonDetailsModel } from 'src/app/core/models/lesson.units/lesson.details.model';
import { LessonService } from 'src/app/core/service/lesson.service';
import { NodeService } from 'src/app/core/service/student-view.service';

@Component({
  selector: 'app-lesson-view',
  templateUrl: './lesson-view.component.html',
  styleUrls: ['./lesson-view.component.sass'],
  providers: [ConfirmationService],
})
export class LessonViewComponent {

  listoflessons: LessonDetailsModel[] = [];
  files!: TreeNode[];

  constructor(
    private _router: Router,
    private _spinner : NgxSpinnerService,
    private _lessonService : LessonService,
    private _confirmationService: ConfirmationService,
    private _toastr: ToastrService,
    private nodeService: NodeService,
  ){}


  //------------------------------------------------------------------------------
  // @ Lifecycle hooks
  //------------------------------------------------------------------------------

  ngOnInit(){
    this.getAllLessons();
    this.getGrade();
 }


 async getGrade() {
  this.nodeService.getFiles().then((data) => (this.files = data));
 }

  /**
	 * @param {}
	 * @service getAllLessons
	 * @returns {Promise<void>}
	 */

  selectedLessonIndex = 0;
  async getAllLessons() {
    try {
      this._spinner.show();

      let response = await this._lessonService.getLessonUnits();
      this.listoflessons = response;
      console.log(this.listoflessons);
      
      this._spinner.hide();
    } catch (error) {
      this._spinner.hide();
    }
  }

  async navigateToResource(id: number) : Promise<void>{    
    if (id > 0) {
      this._router.navigate(['/student/student-lesson/lesson-view/lesson-resources', id]);
    }else{
      this._router.navigate(['/student/student-lesson/lesson-view/lesson-resources', 0]);
    }
  }

}
