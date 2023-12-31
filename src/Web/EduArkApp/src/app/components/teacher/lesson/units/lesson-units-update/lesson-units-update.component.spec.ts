import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LessonUnitsUpdateComponent } from './lesson-units-update.component';

describe('LessonUnitsUpdateComponent', () => {
  let component: LessonUnitsUpdateComponent;
  let fixture: ComponentFixture<LessonUnitsUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LessonUnitsUpdateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LessonUnitsUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
