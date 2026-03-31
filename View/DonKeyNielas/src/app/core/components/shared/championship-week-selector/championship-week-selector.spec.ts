import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChampionshipWeekSelector } from './championship-week-selector';

describe('ChampionshipWeekSelector', () => {
  let component: ChampionshipWeekSelector;
  let fixture: ComponentFixture<ChampionshipWeekSelector>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ChampionshipWeekSelector],
    }).compileComponents();

    fixture = TestBed.createComponent(ChampionshipWeekSelector);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
