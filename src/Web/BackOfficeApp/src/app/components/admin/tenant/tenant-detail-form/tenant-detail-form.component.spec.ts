import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TenantDetailFormComponent } from './tenant-detail-form.component';

describe('TenantDetailFormComponent', () => {
  let component: TenantDetailFormComponent;
  let fixture: ComponentFixture<TenantDetailFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TenantDetailFormComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TenantDetailFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
