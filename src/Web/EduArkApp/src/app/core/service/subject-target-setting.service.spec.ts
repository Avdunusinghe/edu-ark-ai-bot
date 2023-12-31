import { TestBed } from '@angular/core/testing';

import { SubjectTargetSettingService } from './subject-target-setting.service';

describe('SubjectTargetSettingService', () => {
  let service: SubjectTargetSettingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SubjectTargetSettingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
