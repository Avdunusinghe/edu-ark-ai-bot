import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClassDetailFormComponent } from './class-detail-form.component';

describe('ClassDetailFormComponent', () => {
  let component: ClassDetailFormComponent;
  let fixture: ComponentFixture<ClassDetailFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClassDetailFormComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClassDetailFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
