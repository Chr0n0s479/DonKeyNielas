import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddMatchRow } from './add-match-row';

describe('AddMatchRow', () => {
  let component: AddMatchRow;
  let fixture: ComponentFixture<AddMatchRow>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddMatchRow],
    }).compileComponents();

    fixture = TestBed.createComponent(AddMatchRow);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
