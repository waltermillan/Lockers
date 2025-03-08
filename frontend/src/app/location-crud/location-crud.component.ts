import { Component, OnInit } from '@angular/core';
import { Location } from '../models/location.models';
import { LocationService } from '../services/location.service';

@Component({
  selector: 'app-location-crud',
  templateUrl: './location-crud.component.html',
  styleUrl: './location-crud.component.css'
})
export class LocationCrudComponent implements OnInit{

  locations:Location[] = [];
  isPopupOpen:boolean = false;
  selectedLocation: Location | null = null;

  newLocation:Location = {
    id: 0,
    address: '',
    postalCode: 0
  }

  constructor(private locationService: LocationService) {

  }

  ngOnInit(): void {
    this.getAllLocations();
  }

  onSubmit(){
    console.log(JSON.stringify(this.newLocation));
    this.addLocation();
  }

  addLocation(){
      this.locationService.addLocation(this.newLocation).subscribe({
        next: (data) => {
          this.getAllLocations();
          alert('Locaion addedd successfully.');
          console.log('Locaion addedd successfully.');
        },
        error: (error) => {
          console.error('Error adding new location.',error)
        }
      })
  }

  getAllLocations(){
    this.locationService.getAllLocations().subscribe({
      next: (data) => {
        this.locations = data;
        console.log('Loading all locations.');
      },
      error: (error) => {
        console.error('Error adding new location.',error)
      }
    });
  }

  deleteLocation(id:number){
    this.locationService.deleteLocation(id).subscribe({
      next: (data) => {
        alert('Location deleted successfully.');
        console.log('Location deleted successfully.');
        this.getAllLocations();
      },
      error: (error) => {
        console.error('Error deleting location', error);
      }
    })
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
