import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubjectTeacherDetailComponent } from './subject-teacher-detail.component';

describe('SubjectTeacherDetailComponent', () => {
  let component: SubjectTeacherDetailComponent;
  let fixture: ComponentFixture<SubjectTeacherDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SubjectTeacherDetailComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SubjectTeacherDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
