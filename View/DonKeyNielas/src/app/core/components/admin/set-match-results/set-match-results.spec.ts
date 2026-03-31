import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SetMatchResults } from './set-match-results';

describe('SetMatchResults', () => {
  let component: SetMatchResults;
  let fixture: ComponentFixture<SetMatchResults>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SetMatchResults],
    }).compileComponents();

    fixture = TestBed.createComponent(SetMatchResults);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
