// Componente hijo: LockerUpdateComponent
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { Location } from '../models/location.models';
import { LocationService } from '../services/location.service';

@Component({
  selector: 'app-location-update',
  templateUrl: './location-update.component.html',
  styleUrl: './location-update.component.css'
})
export class LocationUpdateComponent {
  @Input() location: Location | null = null; // Receives the location from the parent
  @Output() close = new EventEmitter<void>();

  updLocation:Location = {
    id: 0,
    address: '',
    postalCode: 0,
  }

  locations: Location[] = [];

  constructor(private locationService: LocationService) 
  {
  }

  ngOnInit(): void {
    this.getLocationById();
    this.getAllLocations();
  }


  onSubmit(){
    this.updateLocation();
  }

  getLocationById()
  {
    if(this.location){
      this.updLocation.id = this.location.id;
      this.updLocation.address = this.location.address;
      this.updLocation.postalCode = this.location.postalCode;
    }
  }

  getAllLocations():void{
    this.locationService.getAllLocations().subscribe({
      next: (data) => {
        this.locations = data;
      },
      error: (error) => {
        console.error('Error loading locations.');
      }
    });
  }

  updateLocation() {
    console.log('Updating location', this.location);
    this.locationService.updateLocation(this.updLocation).subscribe({
      next: (data) => {
        alert('Location updated successfully.');
        console.log('location updated successfully.');
      },
      error: (error) => {
        if(error.status === 400)
          alert('the location already exists!');
        console.error('Error updating new location', error);
      }
    })
  }

  closePopup() {
    this.close.emit();
  }

}
