import { TestBed } from '@angular/core/testing';

import { StudentTargetSettingService } from './student-target-setting.service';

describe('StudentTargetSettingService', () => {
  let service: StudentTargetSettingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StudentTargetSettingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
