import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Quinielas } from './quinielas';

describe('Quinielas', () => {
  let component: Quinielas;
  let fixture: ComponentFixture<Quinielas>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Quinielas],
    }).compileComponents();

    fixture = TestBed.createComponent(Quinielas);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
