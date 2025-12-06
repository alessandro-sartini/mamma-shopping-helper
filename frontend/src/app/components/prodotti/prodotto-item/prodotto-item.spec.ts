import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProdottoItem } from './prodotto-item';

describe('ProdottoItem', () => {
  let component: ProdottoItem;
  let fixture: ComponentFixture<ProdottoItem>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProdottoItem]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProdottoItem);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
