import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListaDetail } from './lista-detail';

describe('ListaDetail', () => {
  let component: ListaDetail;
  let fixture: ComponentFixture<ListaDetail>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListaDetail]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListaDetail);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
