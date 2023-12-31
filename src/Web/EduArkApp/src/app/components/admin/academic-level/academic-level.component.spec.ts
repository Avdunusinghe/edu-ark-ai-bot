import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AcademicLevelComponent } from './academic-level.component';

describe('AcademicLevelComponent', () => {
  let component: AcademicLevelComponent;
  let fixture: ComponentFixture<AcademicLevelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AcademicLevelComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AcademicLevelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
