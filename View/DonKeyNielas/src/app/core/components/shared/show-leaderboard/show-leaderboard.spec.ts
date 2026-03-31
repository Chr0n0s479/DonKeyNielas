import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowLeaderboard } from './show-leaderboard';

describe('ShowLeaderboard', () => {
  let component: ShowLeaderboard;
  let fixture: ComponentFixture<ShowLeaderboard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ShowLeaderboard],
    }).compileComponents();

    fixture = TestBed.createComponent(ShowLeaderboard);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
