import { TestBed } from '@angular/core/testing';

import { SpinnerMessageService } from './spinner-message.service';

describe('SpinnerMessageService', () => {
  let service: SpinnerMessageService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SpinnerMessageService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
