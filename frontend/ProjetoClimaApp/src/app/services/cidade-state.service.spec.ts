import { TestBed } from '@angular/core/testing';

import { CidadeStateService } from './cidade-state.service';

describe('CidadeStateService', () => {
  let service: CidadeStateService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CidadeStateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
