import { Component, OnInit } from '@angular/core';
import { Customer } from '../models/customer.models';
import { LockerDTO } from '../models/locker-dto.models';
import { Locker } from '../models/locker.models';
import { RentDTO } from '../models/rent-dto.models';
import { User } from '../models/user.model';
import { AuthService } from '../services/auth.service';
import { CustomerService } from '../services/customer.service';
import { LockerService } from '../services/locker.service';
import { RentService } from '../services/rent.service';
import { CommonService } from '../services/common.service';

@Component({
  selector: 'app-rent-crud',
  templateUrl: './rent-crud.component.html',
  styleUrls: ['./rent-crud.component.css']
})
export class RentCrudComponent implements OnInit {

  user: User | null = null;
  rentsDTO: RentDTO[] = [];
  customers: Customer[] = [];
  lockers: LockerDTO[] = [];
  locker:Locker = new Locker();

  newRent: RentDTO = {
    id: 0,
    idCustomer: 1,
    customer: '',
    idLocker: 1,
    locker: '',
    rentalDate: new Date(),
    returnDate: '',
  };

  constructor(private customerService: CustomerService,
              private lockerService: LockerService,
              private rentService: RentService,
              private authService: AuthService,
              private commonService: CommonService) {}

  ngOnInit(): void {
    this.getAllCustomers();
    this.getAllLockers();
    this.getAllRents();
  }

  getAllRents(): void {
    this.rentService.getAllRents().subscribe({
      next: (data) => {
        this.rentsDTO = data;
        console.log('Rents loading successfully.');
      },
      error: (error) => {
        console.error('Error loading rents.', error);
      }
    });
  }

  getAllCustomers(): void {
    this.customerService.getAllCustomers().subscribe({
      next: (data) => {
        this.customers = data;
        console.log('Customers loading successfully.');
      },
      error: (error) => {
        console.error('Error loading customers.', error);
      }
    });
  }

  getAllLockers(): void {
    this.lockerService.getAllLockers().subscribe({
      next: (data) => {
        this.lockers = data.filter(locker => !locker.rented);

      if (this.lockers.length > 0) {
        this.newRent.idLocker = this.lockers[0].id; //Assigns the id of the first available locker
      }

        console.log('Lockers loading successfully.');
      },
      error: (error) => {
        console.error('Error loading lockers.', error);
      }
    });
  }

  onSubmit(): void {

    if (this.formValid()) {
      if (confirm('Are you sure you want to perform this action?')) {
        let rent = {
          id: 0,
          idCustomer: this.newRent.idCustomer,
          idLocker: this.newRent.idLocker,
          rentalDate: this.newRent.rentalDate,
          returnDate: this.newRent.returnDate,
          userName: this.authService.getUserLogged()
        };

        this.rentService.addRent(rent).subscribe({
          next: (data) => {
            console.log('Rent added successfully.');
            this.getLockerById(rent.idLocker);
          },
          error: (error) => {
            console.error('Error adding rent.', error);
          }
        });
      }
    } else {
      alert('Please ensure all fields are filled correctly.');
    }
  }

  getLockerById(id:number ){
    this.lockerService.getLockerById(id).subscribe({
      next: (data) => {
        this.locker = data;
        this.locker.rented = !this.locker.rented ;
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
        this.getAllRents();
        this.getAllLockers();
      },
      error: (error) => {
        console.log('Error updating locker data', error);
      }
    });
  }

  formValid(): boolean {
    const rentalDay = this.newRent.rentalDate;
    const returnDay = this.newRent.returnDate;
    
    if (!rentalDay || !returnDay) {
      return false;
    }
    
    const today = new Date();
    if (rentalDay < today) {
      return false;
    }

    return this.newRent.idCustomer != null && this.newRent.idLocker != null && rentalDay != null && returnDay != null;
  }

  setReturnDay() {
    this.newRent.returnDate = this.commonService.getReturnDateString(this.newRent.rentalDate);
  }
  
}
