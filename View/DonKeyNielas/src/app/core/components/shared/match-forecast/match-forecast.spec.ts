import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MatchForecast } from './match-forecast';

describe('MatchForecast', () => {
  let component: MatchForecast;
  let fixture: ComponentFixture<MatchForecast>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MatchForecast],
    }).compileComponents();

    fixture = TestBed.createComponent(MatchForecast);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
