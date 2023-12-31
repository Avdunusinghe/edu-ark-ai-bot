import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubjectTargetSettingComponent } from './subject-target-setting.component';

describe('SubjectTargetSettingComponent', () => {
  let component: SubjectTargetSettingComponent;
  let fixture: ComponentFixture<SubjectTargetSettingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SubjectTargetSettingComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SubjectTargetSettingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
