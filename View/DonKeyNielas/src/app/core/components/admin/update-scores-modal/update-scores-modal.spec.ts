import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateScoresModal } from './update-scores-modal';

describe('UpdateScoresModal', () => {
  let component: UpdateScoresModal;
  let fixture: ComponentFixture<UpdateScoresModal>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UpdateScoresModal],
    }).compileComponents();

    fixture = TestBed.createComponent(UpdateScoresModal);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
