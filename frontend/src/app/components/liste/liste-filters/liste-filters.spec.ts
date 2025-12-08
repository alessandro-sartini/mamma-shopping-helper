import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListeFilters } from './liste-filters';

describe('ListeFilters', () => {
  let component: ListeFilters;
  let fixture: ComponentFixture<ListeFilters>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListeFilters]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListeFilters);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
