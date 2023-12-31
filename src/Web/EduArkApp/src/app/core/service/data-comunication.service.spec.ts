import { TestBed } from '@angular/core/testing';

import { DataComunicationService } from './data-comunication.service';

describe('DataComunicationService', () => {
  let service: DataComunicationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DataComunicationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
