import { Component } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { EMPTY, Observable } from 'rxjs';
import { Upload } from 'src/app/core/models/common/upload';
import { LessonDetailsModel } from 'src/app/core/models/lesson.units/lesson.details.model';
import { LessonService } from 'src/app/core/service/lesson.service';
import { SpinnerMessageService } from 'src/app/core/service/spinner-message.service';

@Component({
  selector: 'app-lesson-units-add',
  templateUrl: './lesson-units-add.component.html',
  styleUrls: ['./lesson-units-add.component.sass'],
  providers: [ConfirmationService, DialogService],
})
export class LessonUnitsAddComponent {
  //core data properties
  lessonUnitsAddForm: UntypedFormGroup;
  lessonId : number = 0;  
  currentLessonDetailModel: LessonDetailsModel;
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
    private _spinnerMessageService: SpinnerMessageService,
  ){
  }

	// -----------------------------------------------------------------------------------------------------
	// @ Lifecycle hooks
	// -----------------------------------------------------------------------------------------------------

	/**
	 * On init
	 */

  async ngOnInit() {
    await this.createLessonAddDetailForm();

    this._route.params.subscribe(async (params) => {
			this.lessonId = parseInt(params['id']);
       if(this.lessonId > 0){
        await this.getByIdLesson();
       }
       
			});
		
	}

  async createLessonAddDetailForm() : Promise<void> {
    try{
      this.lessonUnitsAddForm = this._untypedFormBuilder.group({
        lessonName: ['', Validators.required],
        lessonDescription: ['', Validators.required],
        lessonGrade: ['', Validators.required],
        lessonSubject: ['', Validators.required],
      });
    }catch (error) {

    }
  }

  async getByIdLesson() : Promise<void>{
    try {
      this._spinner.show();
      let response = await this._lessonService.getByIdLessonUnits(this.lessonId);
      this.lessonUnitsAddForm.patchValue(response);

      this._spinner.hide();

    } catch (error) {
      this._spinner.hide();
    }
  }

  async saveLessonUnit() {
    try {
      this._spinner.show();
      let response = await this._lessonService.saveLessonUnit(this.lessonUnitsAddForm.value);

      if (response.succeeded) {
        this._toastr.success(response.successMessage, 'SUCCESS');
        this._router.navigate(['/teacher/lesson/units']);
      } else {
        response.errors.forEach((error) => {
          this._toastr.error(error, 'ERROR');
        })
      }

      this._spinner.hide();
    } catch (error) {
      this._spinner.hide();
    }
  }

  // This is a one of the correct save form (add)

  // async saveLessonUnit() {
  //   try {
  //     this._spinner.show();
  //     var lessModel = this.lessonUnitsAddForm.getRawValue();
  //     let response = await this._lessonService.saveLessonUnit(lessModel);
      
  //     console.log(lessModel);

  //     if (response.succeeded) {
  //       this._spinner.hide();
  //       this._toastr.success(response.successMessage, 'done')
  //       this._router.navigate(['/teacher/lesson/units']);
  //     }else{
  //       this._spinner.hide();
  //       response.errors.forEach((error: any) => {
  //         this._toastr.error(error, 'error');
  //       })
  //     }
  //   } catch (error) {
  //     this._spinner.hide();
  //   }
  // }

  get Id() : number {
    return this.lessonUnitsAddForm.value.id;
  }


  /**
	 * Method of get lesson file upload (video,audio, and text) 
	 *
	 * @param {any} event
	 * @returns {Observable<Upload>}
	 */

  upload$ : Observable<Upload> = EMPTY;
  precetage : any;

  onFileChange(event : any){
    let file = event.srcElement;
    const formData = new FormData();

    formData.set('id', this.currentLessonDetailModel.id.toString());

    if (file.files.length > 0) {
      this._spinnerMessageService.sendData('UPLOADING RESOURCE FILES..');
      this._spinner.show();

      for (let index = 0; index < file.files.length; index++) {
        formData.append('file', file.files[index], file.files[index].name);
        
      }
      console.log('====================================');
			console.log(formData);
			console.log('====================================');

      this._lessonService.uploadLessonFile(formData).subscribe(
        (response) => {
          this.precetage = response;

          if (response.state === 'DONE') {
            this._spinner.hide();
            this._spinnerMessageService.sendData('');
            this._toastr.success('LESSON FILE HAS BEEN UPLOADED SUCCESSFULLY.');
            this.getByIdLesson();
          }
        },
        (error) => {
          console.log(error);
          this._spinner.hide();
          this._spinnerMessageService.sendData('');
          this._toastr.error('NETWORK ERROR HAS BEEN OCCURED!, PLEASE TRY AGAIN');
        }
      );

    }
  }
}
