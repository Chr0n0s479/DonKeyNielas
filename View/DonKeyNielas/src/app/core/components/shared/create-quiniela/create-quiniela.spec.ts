import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateQuiniela } from './create-quiniela';

describe('CreateQuiniela', () => {
  let component: CreateQuiniela;
  let fixture: ComponentFixture<CreateQuiniela>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateQuiniela],
    }).compileComponents();

    fixture = TestBed.createComponent(CreateQuiniela);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
