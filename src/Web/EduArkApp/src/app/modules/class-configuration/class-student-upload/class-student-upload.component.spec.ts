import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClassStudentUploadComponent } from './class-student-upload.component';

describe('ClassStudentUploadComponent', () => {
  let component: ClassStudentUploadComponent;
  let fixture: ComponentFixture<ClassStudentUploadComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClassStudentUploadComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClassStudentUploadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
