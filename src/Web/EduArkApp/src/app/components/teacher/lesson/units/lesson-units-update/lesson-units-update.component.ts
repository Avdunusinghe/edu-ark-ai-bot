import { Component } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { LessonService } from 'src/app/core/service/lesson.service';

@Component({
  selector: 'app-lesson-units-update',
  templateUrl: './lesson-units-update.component.html',
  styleUrls: ['./lesson-units-update.component.sass'],
  providers: [ConfirmationService, DialogService]
})
export class LessonUnitsUpdateComponent {
  lessonUnitsUpdateForm : UntypedFormGroup;
  lessonId : number;
  /**
	 * Constructor
   * @param {UntypedFormBuilder} _untypedFormBuilder
   * @param {NgxSpinnerService} _spinner
   * @param {ToastrService} _toastr
   * @param {Router} _router
   */

  constructor (
    private _untypedFormBuilder: UntypedFormBuilder,
    private _spinner: NgxSpinnerService,
    private _lessonService: LessonService,
    private _toastr: ToastrService,
    private _router: Router,
    private _route: ActivatedRoute,
  ){}

  // -----------------------------------------------------------------------------------------------------
	// @ Lifecycle hooks
	// -----------------------------------------------------------------------------------------------------

	/**
	 * On init
	 */

  async ngOnInit() {
    this._route.params.subscribe(async (params) => {
			this.lessonId = parseInt(params['id']);
        console.log("NG ON INIT METHOS WORKING");
        await this.getByIdLesson();
			});
		await this.updateLessonAddDetailForm();
	}

  async getByIdLesson() : Promise<void>{
    try {
      this._spinner.show();
      let response = this._lessonService.getByIdLessonUnits(this.lessonId);
      this.lessonUnitsUpdateForm.patchValue(response);
      this._spinner.hide();

    } catch (error) {
      this._spinner.hide();
      console.error('Error fetching lesson:', error);
    }
  }

  async updateLessonAddDetailForm() : Promise<void> {
    try {
      this._spinner.show();
      const updateLessonModel = this.lessonUnitsUpdateForm.getRawValue();
      const response = await this._lessonService
    } catch (error) {
      this._spinner.hide();
    }
  }

  // async updateLessonAddDetailForm() : Promise<void> {
  //   try {
  //     this._spinner.show();
  //     const lessonId = this.lessonUnitsUpdateForm.getRawValue();
  //     const updateLessonModel = this.lessonUnitsUpdateForm.getRawValue();
  //     const response = await this._lessonService.updateLessonUnit(lessonId, updateLessonModel);
      
  //     if (response.succeeded) {
  //       this._spinner.hide();
  //       this._toastr.success(response.successMessage, 'UPDATED COMPLETED');
  //       this._router.navigate(['/teacher/lesson/units']);
  //     } else {
  //       this._spinner.hide();
  //       response.errors.forEach((error: any) => {
  //         this._toastr.error(error, 'ERROR!, PLEASE CHECK AGAIN.')
  //       })
  //     }

  //   } catch (error) {
  //     this._spinner.hide();
  //   }
  // }
}
