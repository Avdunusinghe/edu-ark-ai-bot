import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LessonUnitsAddComponent } from './lesson-units-add.component';

describe('LessonUnitsAddComponent', () => {
  let component: LessonUnitsAddComponent;
  let fixture: ComponentFixture<LessonUnitsAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LessonUnitsAddComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LessonUnitsAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
