import { TestBed } from '@angular/core/testing';

import { Prodotto } from './prodotto';

describe('Prodotto', () => {
  let service: Prodotto;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Prodotto);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
