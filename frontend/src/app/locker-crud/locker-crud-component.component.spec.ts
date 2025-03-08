import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LockerCrudComponentComponent } from './locker-crud-component.component';

describe('LockerCrudComponentComponent', () => {
  let component: LockerCrudComponentComponent;
  let fixture: ComponentFixture<LockerCrudComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LockerCrudComponentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LockerCrudComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
