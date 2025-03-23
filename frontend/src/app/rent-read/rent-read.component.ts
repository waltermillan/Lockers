import { Component, OnInit } from '@angular/core';
import { RentDTO } from '../models/rent-dto.models';
import { RentService} from '../services/rent.service';
import { Locker } from '../models/locker.models';
import { LockerService } from '../services/locker.service';

@Component({
  selector: 'app-rent-read',
  templateUrl: './rent-read.component.html',
  styleUrl: './rent-read.component.css'
})
export class RentReadComponent implements OnInit {

  
  rents: RentDTO[] = [];
  locker: Locker = new Locker();

  constructor(private rentService: RentService,
              private lockerService: LockerService) 
  {
    
  }

  ngOnInit(): void {
    this.getAllRents();
  }

  getAllRents(){
    this.rentService.getAllRents().subscribe({
      next: (data) => {
          this.rents = data;
          //console.log('Rents added successfully');
      },
      error: (error) => {
        console.error('error adding rents', error);
      }
    });
  }
  
  deleteRent(id:number, lockerid:number){
    this.rentService.deleteRent(id).subscribe({
      next: (data) => {
          alert('Rents deleted successfully');        
          this.getAllRents();
          this.getLockerById(lockerid)
          console.log('Rents deleted successfully');
      },
      error: (error) => {
        console.error('error deleting rents', error);
      }
    });
  }

  getLockerById(id:number ){
    this.lockerService.getLockerById(id).subscribe({
      next: (data) => {
        this.locker = data;
        this.locker.rented = !this.locker.rented;
        this.updateLockerState();
      },
      error: (error) => {
        console.log('Error getting locker data', error);
      }
    });

    
  }

  updateLockerState(){
    this.lockerService.updateLockerRented(this.locker).subscribe({
      next: () => {
        console.log('State Rented for locker added.')
      },
      error: (error) => {
        console.log('Error updating locker data', error);
      }
    });
  }
}
