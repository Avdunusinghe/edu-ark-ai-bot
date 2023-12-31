import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExamMarkSettingComponent } from './exam-mark-setting.component';

describe('ExamMarkSettingComponent', () => {
  let component: ExamMarkSettingComponent;
  let fixture: ComponentFixture<ExamMarkSettingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExamMarkSettingComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ExamMarkSettingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
