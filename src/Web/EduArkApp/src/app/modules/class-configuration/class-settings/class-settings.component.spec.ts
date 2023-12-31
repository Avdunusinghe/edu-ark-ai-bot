import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClassSettingsComponent } from './class-settings.component';

describe('ClassSettingsComponent', () => {
  let component: ClassSettingsComponent;
  let fixture: ComponentFixture<ClassSettingsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClassSettingsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClassSettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
