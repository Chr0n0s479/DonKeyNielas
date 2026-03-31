import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddRoundModal } from './add-round-modal';

describe('AddRoundModal', () => {
  let component: AddRoundModal;
  let fixture: ComponentFixture<AddRoundModal>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddRoundModal],
    }).compileComponents();

    fixture = TestBed.createComponent(AddRoundModal);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
