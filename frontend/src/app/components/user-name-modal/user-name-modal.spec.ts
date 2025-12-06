import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserNameModal } from './user-name-modal';

describe('UserNameModal', () => {
  let component: UserNameModal;
  let fixture: ComponentFixture<UserNameModal>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserNameModal]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserNameModal);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
