import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListeList } from './liste-list';

describe('ListeList', () => {
  let component: ListeList;
  let fixture: ComponentFixture<ListeList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListeList]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListeList);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
