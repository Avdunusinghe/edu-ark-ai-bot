import { TestBed } from '@angular/core/testing';

import { LessonUnitAddServiceService } from './lesson-unit-add-service.service';

describe('LessonUnitAddServiceService', () => {
  let service: LessonUnitAddServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LessonUnitAddServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
