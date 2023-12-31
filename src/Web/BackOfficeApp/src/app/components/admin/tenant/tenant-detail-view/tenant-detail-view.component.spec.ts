import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TenantDetailViewComponent } from './tenant-detail-view.component';

describe('TenantDetailViewComponent', () => {
  let component: TenantDetailViewComponent;
  let fixture: ComponentFixture<TenantDetailViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TenantDetailViewComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TenantDetailViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
