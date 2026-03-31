import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminButtonModule } from './admin-button-module';

describe('AdminButtonModule', () => {
  let component: AdminButtonModule;
  let fixture: ComponentFixture<AdminButtonModule>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminButtonModule],
    }).compileComponents();

    fixture = TestBed.createComponent(AdminButtonModule);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
