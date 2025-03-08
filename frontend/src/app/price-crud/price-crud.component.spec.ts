import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PriceCrudComponent } from './price-crud.component';

describe('PriceCrudComponent', () => {
  let component: PriceCrudComponent;
  let fixture: ComponentFixture<PriceCrudComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PriceCrudComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PriceCrudComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
