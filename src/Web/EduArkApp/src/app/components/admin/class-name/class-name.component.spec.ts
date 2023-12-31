import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClassNameComponent } from './class-name.component';

describe('ClassNameComponent', () => {
  let component: ClassNameComponent;
  let fixture: ComponentFixture<ClassNameComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClassNameComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClassNameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
