import { Component, OnInit } from '@angular/core';
import { RentDTO } from '@models/rent-dto.models';
import { RentService} from '@services/rent.service';
import { Locker } from '@models/locker.models';
import { LockerService } from '@services/locker.service';
import { DialogService } from '@services/dialog-service.service';
import { DialogType } from 'enums/dialog-type.enum';
import { ExportService } from '@services/export.service'; 

@Component({
  selector: 'app-rent-read',
  templateUrl: './rent-read.component.html',
  styleUrl: './rent-read.component.css'
})
export class RentReadComponent implements OnInit {

  
  rents: RentDTO[] = [];
  locker: Locker = new Locker();
  total: number = 0;

  constructor(private rentService: RentService,
              private lockerService: LockerService,
              private exportService: ExportService,
              private dialogService: DialogService){
    
  }

  ngOnInit(): void {
    this.getAllRents();
    this.getTotalRented();
  }

  getAllRents(){
    this.rentService.getAll().subscribe({
      next: (data) => {
          this.rents = data;
      },
      error: (error) => {
        console.error('Error adding rents.', error);
        this.dialogService.open('Error adding rents.', DialogType.Failure);
      }
    });
  }
  
  deleteRent(id:number, lockerid:number){
    this.rentService.delete(id).subscribe({
      next: (data) => { 
          this.dialogService.open('Rent deleted successfully.', DialogType.Success);   
          this.getAllRents();
          this.getLockerById(lockerid);
      },
      error: (error) => {
        console.error('error deleting rents', error);
        this.dialogService.open('error deleting rents.', DialogType.Failure);
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
        console.log('Error getting locker data.', error);
        this.dialogService.open('Error getting locker data.', DialogType.Failure);
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
        this.dialogService.open('Error updating locker data.', DialogType.Failure);
      },
      complete: () => {
        this.getTotalRented(); 
      }
    });
  }

  getTotalRented() {
    this.lockerService.getAllLockers().subscribe({
      next: (data) => {
        const prices = data.filter(a => a.rented).map(l => l.price);
        this.total = prices.reduce((sum, price) => sum + price, 0);
      },
      error: (error) => {
        console.error('Error getting the total price.');
      }
    });
  }  

  export(){
    this.exportService.exportHtmlTableByIdToPDF('tblLockersRented');
  }
}
