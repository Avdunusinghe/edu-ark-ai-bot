import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClassStudentProfileComponent } from './class-student-profile.component';

describe('ClassStudentProfileComponent', () => {
  let component: ClassStudentProfileComponent;
  let fixture: ComponentFixture<ClassStudentProfileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClassStudentProfileComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClassStudentProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
