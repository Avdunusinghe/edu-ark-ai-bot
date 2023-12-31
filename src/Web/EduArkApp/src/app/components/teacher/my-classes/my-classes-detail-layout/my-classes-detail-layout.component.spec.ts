import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyClassesDetailLayoutComponent } from './my-classes-detail-layout.component';

describe('MyClassesDetailLayoutComponent', () => {
  let component: MyClassesDetailLayoutComponent;
  let fixture: ComponentFixture<MyClassesDetailLayoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MyClassesDetailLayoutComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MyClassesDetailLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
