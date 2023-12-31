import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LessonResourcesComponent } from './lesson-resources.component';

describe('LessonResourcesComponent', () => {
  let component: LessonResourcesComponent;
  let fixture: ComponentFixture<LessonResourcesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LessonResourcesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LessonResourcesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
