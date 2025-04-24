// Componente hijo: LockerUpdateComponent
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { Location } from '@models/location.models';
import { LocationService } from '@services/location.service';
import { DialogService } from '@services/dialog-service.service';
import { DialogType } from 'enums/dialog-type.enum';

@Component({
  selector: 'app-location-update',
  templateUrl: './location-update.component.html',
  styleUrl: './location-update.component.css',
})
export class LocationUpdateComponent {
  @Input() location: Location | null = null; // Receives the location from the parent
  @Output() close = new EventEmitter<void>();

  updLocation: Location = {
    id: 0,
    address: '',
    postalCode: 0,
  };

  locations: Location[] = [];

  constructor(
    private locationService: LocationService,
    private dialogService: DialogService
  ) {}

  ngOnInit(): void {
    this.getLocationById();
    this.getAllLocations();
  }

  onSubmit() {
    this.updateLocation();
  }

  getLocationById() {
    if (this.location) {
      this.updLocation.id = this.location.id;
      this.updLocation.address = this.location.address;
      this.updLocation.postalCode = this.location.postalCode;
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

  updateLocation() {
    console.log('Updating location', this.location);
    this.locationService.update(this.updLocation).subscribe({
      next: (data) => {
        this.dialogService.open(
          'Location updated successfully.',
          DialogType.Success
        );
        console.log('location updated successfully.');
      },
      error: (error) => {
        if (error.status === 400) {
          this.dialogService.open(
            'the location already exists!',
            DialogType.Warning
          );
        } else {
          console.error('Error updating new location', error);
          this.dialogService.open(
            'Error updating new location.',
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
