import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { LockerDTO } from '@models/locker-dto.models';
import { Location } from '@models/location.models';
import { LockerService } from '@services/locker.service';
import { LocationService } from '@services/location.service';
import { PriceService } from '@services/price.service';

import { Locker } from '@models/locker.models';
import { Price } from '@models/price.models';

import { DialogService } from '@services/dialog-service.service';
import { DialogType } from 'enums/dialog-type.enum';

@Component({
  selector: 'app-locker-update',
  templateUrl: './locker-update.component.html',
  styleUrls: ['./locker-update.component.css'],
})
export class LockerUpdateComponent implements OnInit {
  @Input() locker: LockerDTO | null = null; // Receives the locker from the parent
  @Output() close = new EventEmitter<void>();

  updLocker: Locker = {
    id: 0,
    serialNumber: 1,
    idLocation: 1,
    idPrice: 1,
    rented: false,
  };

  locations: Location[] = [];
  prices: Price[] = [];

  constructor(
    private lockerService: LockerService,
    private locationService: LocationService,
    private priceService: PriceService,
    private dialogService: DialogService
  ) {}

  ngOnInit(): void {
    this.getLockerById();
    this.getAllLocations();
    this.getAllPrices();
  }

  onSubmit() {
    this.updateLocker();
  }

  getLockerById() {
    if (this.locker) {
      this.updLocker.id = this.locker.id;
      this.updLocker.serialNumber = this.locker.serialNumber;
      this.updLocker.idLocation = this.locker?.idLocation;
      this.updLocker.idPrice = this.locker?.idPrice;
      this.updLocker.rented = this.locker?.rented;
    }
  }

  getAllLocations(): void {
    this.locationService.getAll().subscribe({
      next: (data) => {
        this.locations = data;
      },
      error: (error) => {
        console.error('Error loading locations.');
        this.dialogService.open('Error loading locations.', DialogType.Failure);
      },
    });
  }

  getAllPrices() {
    this.priceService.getAll().subscribe({
      next: (data: Price[]) => {
        this.prices = data;
      },
      error: (error) => {
        console.error('Error loading prices');
        this.dialogService.open('Error loading prices.', DialogType.Failure);
      },
    });
  }

  updateLocker() {
    console.log('Updating locker', this.locker);
    this.lockerService.updateLocker(this.updLocker).subscribe({
      next: (data) => {
        this.dialogService.open(
          'Locker updated successfully.',
          DialogType.Success
        );
        console.log('Locker updated successfully.');
      },
      error: (error) => {
        alert(error.status);
        if (error.status === 400)
          this.dialogService.open(
            'the locker already exists!',
            DialogType.Warning
          );
        else {
          console.error('Error adding new locker', error);
          this.dialogService.open(
            'Error adding new locker',
            DialogType.Failure
          );
        }
      },
    });
  }

  closePopup() {
    this.close.emit();
  }
}
