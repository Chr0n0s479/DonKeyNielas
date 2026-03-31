import { TestBed } from '@angular/core/testing';

import { Championship } from './championship';

describe('Championship', () => {
  let service: Championship;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Championship);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
