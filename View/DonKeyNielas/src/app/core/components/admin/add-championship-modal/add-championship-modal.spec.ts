import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddChampionshipModal } from './add-championship-modal';

describe('AddChampionshipModal', () => {
  let component: AddChampionshipModal;
  let fixture: ComponentFixture<AddChampionshipModal>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddChampionshipModal],
    }).compileComponents();

    fixture = TestBed.createComponent(AddChampionshipModal);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
