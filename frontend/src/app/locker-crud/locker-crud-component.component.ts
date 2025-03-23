import { Component, OnInit } from '@angular/core';

import { LockerDTO } from '../models/locker-dto.models';
import { Location } from '../models/location.models';
import { LockerService } from '../services/locker.service';
import { LocationService } from '../services/location.service';
import { PriceService } from '../services/price.service';

import { Locker } from '../models/locker.models';
import { Price } from '../models/price.models';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-locker-crud-component',
  templateUrl: './locker-crud-component.component.html',
  styleUrl: './locker-crud-component.component.css'
})
export class LockerCrudComponentComponent implements OnInit {

  lockerForm!: FormGroup;
  lockers:LockerDTO[] = [];
  locations: Location[] = [];
  prices: Price[] = [];
  selectedLocation:number = 1;
  selectedPrice:number = 1;
  selectedRented:number = 1;

  selectedLocker: LockerDTO | null = null;
  isPopupOpen = false; 

  newLocker:Locker = {
    id: 0,
    serialNumber: 1,
    idLocation: 1,
    idPrice: 1,
    rented: false
  }

  constructor(private lockerService: LockerService,
              private locationService: LocationService,
              private priceService: PriceService
  ) {
  }

  ngOnInit(): void {
    this.getAllLockers();
    this.getAllLocations();
    this.getAllPrices();
  }

  getAllLockers(){
    return this.lockerService.getAllLockers().subscribe({
      next: (data: LockerDTO[]) => {
        this.lockers = data.sort((a, b) => a.location.localeCompare(b.location));
      },
      error: (error) => {
        console.error('Error loading lockers.', error);
      }
    });
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

  getAllPrices(){
    this.priceService.getAllPrices().subscribe({
      next: (data: Price[]) => {
        this.prices = data;
      },
      error: (error) => {
        console.error('Error loading prices');
      }
    });
  }

  onSubmit(){
    this.addLocker();
  }

  addLocker(){

    this.lockerService.addLocker(this.newLocker).subscribe({
      next: (data) => {
        this.getAllLockers();
        console.log('New locker added successfully.');
      },
      error: (error) => {
        if(error.status === 400)
          alert('the locker already exists!');
        console.error('Error adding new locker', error);
      }
    });
  }

  updateLocker(locker: LockerDTO) {
    this.selectedLocker = locker;
    this.isPopupOpen = true;
  }

  closePopup() {
    this.isPopupOpen = false;
    this.selectedLocker = null;
    this.getAllLockers();
  }

  deleteLocker(id:number){
    this.lockerService.deleteLocker(id).subscribe({
      next: (data) => {
        this.getAllLockers();
        alert('Locker deleted successfully');
        console.log('Locker deleted successfully');
      },
      error: (error) => {
        console.error('Locker deleted failed', error);
      }
    });
  }
}
