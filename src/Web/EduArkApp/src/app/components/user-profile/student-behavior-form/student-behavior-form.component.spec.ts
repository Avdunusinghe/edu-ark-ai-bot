import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentBehaviorFormComponent } from './student-behavior-form.component';

describe('StudentBehaviorFormComponent', () => {
  let component: StudentBehaviorFormComponent;
  let fixture: ComponentFixture<StudentBehaviorFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StudentBehaviorFormComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StudentBehaviorFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
