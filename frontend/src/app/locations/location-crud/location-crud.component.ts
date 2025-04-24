import { Component, OnInit } from '@angular/core';
import { Location } from '@models/location.models';
import { LocationService } from '@services/location.service';

import { DialogService } from '@services/dialog-service.service';
import { DialogType } from 'enums/dialog-type.enum';

@Component({
  selector: 'app-location-crud',
  templateUrl: './location-crud.component.html',
  styleUrl: './location-crud.component.css',
})
export class LocationCrudComponent implements OnInit {
  locations: Location[] = [];
  isPopupOpen: boolean = false;
  selectedLocation: Location | null = null;

  newLocation: Location = {
    id: 0,
    address: '',
    postalCode: 0,
  };

  constructor(
    private locationService: LocationService,
    private dialogService: DialogService
  ) {}

  ngOnInit(): void {
    this.getAllLocations();
  }

  onSubmit() {
    console.log(JSON.stringify(this.newLocation));
    this.addLocation();
  }

  addLocation() {
    this.locationService.add(this.newLocation).subscribe({
      next: (data) => {
        this.getAllLocations();
        this.dialogService.open(
          'Locaion addedd successfully.',
          DialogType.Success
        );
        console.log('Locaion addedd successfully.');
      },
      error: (error) => {
        console.error('Error adding new location.', error);
        this.dialogService.open(
          'Error adding new location.',
          DialogType.Failure
        );
      },
    });
  }

  getAllLocations() {
    this.locationService.getAll().subscribe({
      next: (data) => {
        this.locations = data;
        console.log('Loading all locations.');
      },
      error: (error) => {
        console.error('Error adding new location.', error);
        this.dialogService.open(
          'Error adding new location.',
          DialogType.Failure
        );
      },
    });
  }

  deleteLocation(id: number) {
    this.locationService.delete(id).subscribe({
      next: (data) => {
        this.dialogService.open(
          'Location deleted successfully.',
          DialogType.Success
        );
        console.log('Location deleted successfully.');
        this.getAllLocations();
      },
      error: (error) => {
        console.error('Error deleting location.', error);
        this.dialogService.open('Error deleting location.', DialogType.Failure);
      },
    });
  }

  updateLocation(location: Location) {
    this.selectedLocation = location;
    this.isPopupOpen = true;
  }

  closePopup() {
    this.isPopupOpen = false;
    this.selectedLocation = null;
    this.getAllLocations();
  }
}
