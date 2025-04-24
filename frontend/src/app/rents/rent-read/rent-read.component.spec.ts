import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RentReadComponent } from './rent-read.component';

describe('RentReadComponent', () => {
  let component: RentReadComponent;
  let fixture: ComponentFixture<RentReadComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RentReadComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RentReadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
