import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RentCrudComponent } from './rent-crud.component';

describe('RentCrudComponent', () => {
  let component: RentCrudComponent;
  let fixture: ComponentFixture<RentCrudComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RentCrudComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RentCrudComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
