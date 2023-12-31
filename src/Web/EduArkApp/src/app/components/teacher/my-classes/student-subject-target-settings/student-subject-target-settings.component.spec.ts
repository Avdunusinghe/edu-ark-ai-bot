import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentSubjectTargetSettingsComponent } from './student-subject-target-settings.component';

describe('StudentSubjectTargetSettingsComponent', () => {
  let component: StudentSubjectTargetSettingsComponent;
  let fixture: ComponentFixture<StudentSubjectTargetSettingsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StudentSubjectTargetSettingsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StudentSubjectTargetSettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
