import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClassStudentsComponent } from './class-students.component';

describe('ClassStudentsComponent', () => {
  let component: ClassStudentsComponent;
  let fixture: ComponentFixture<ClassStudentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClassStudentsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClassStudentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
