import { TestBed } from '@angular/core/testing';

import { CidadeFavoritaService } from './cidade-favorita.service';

describe('CidadeFavoritaService', () => {
  let service: CidadeFavoritaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CidadeFavoritaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
