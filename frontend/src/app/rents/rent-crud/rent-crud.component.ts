import { Component, OnInit } from '@angular/core';
import { Customer } from '@models/customer.models';
import { LockerDTO } from '@models/locker-dto.models';
import { Locker } from '@models/locker.models';
import { RentDTO } from '@models/rent-dto.models';
import { User } from '@models/user.model';
import { AuthService } from '@services/auth.service';
import { CustomerService } from '@services/customer.service';
import { LockerService } from '@services/locker.service';
import { RentService } from '@services/rent.service';
import { CommonService } from '@services/common.service';
import { DialogService } from '@services/dialog-service.service';
import { DialogType } from 'enums/dialog-type.enum';

@Component({
  selector: 'app-rent-crud',
  templateUrl: './rent-crud.component.html',
  styleUrls: ['./rent-crud.component.css'],
})
export class RentCrudComponent implements OnInit {
  user: User | null = null;
  rentsDTO: RentDTO[] = [];
  customers: Customer[] = [];
  lockers: LockerDTO[] = [];
  locker: Locker = new Locker();

  newRent: RentDTO = {
    id: 0,
    idCustomer: 1,
    customer: '',
    idLocker: 1,
    locker: '',
    rentalDate: new Date(),
    returnDate: '',
  };

  constructor(
    private customerService: CustomerService,
    private lockerService: LockerService,
    private rentService: RentService,
    private authService: AuthService,
    private dialogService: DialogService,
    private commonService: CommonService
  ) {}

  ngOnInit(): void {
    this.getAllCustomers();
    this.getAllLockers();
    this.getAllRents();
  }

  getAllRents(): void {
    this.rentService.getAll().subscribe({
      next: (data) => {
        this.rentsDTO = data;
      },
      error: (error) => {
        console.error('Error loading rents.', error);
        this.dialogService.open('Error loading rents.', DialogType.Failure);
      },
    });
  }

  getAllCustomers(): void {
    this.customerService.getAll().subscribe({
      next: (data) => {
        this.customers = data;
      },
      error: (error) => {
        console.error('Error loading customers.', error);
        this.dialogService.open('Error loading customers.', DialogType.Failure);
      },
    });
  }

  getAllLockers(): void {
    this.lockerService.getAllLockers().subscribe({
      next: (data) => {
        this.lockers = data.filter((locker) => !locker.rented);

        if (this.lockers.length > 0) {
          this.newRent.idLocker = this.lockers[0].id; //Assigns the id of the first available locker
        }
        console.log('Lockers loading successfully.');
      },
      error: (error) => {
        console.error('Error loading lockers.', error);
        this.dialogService.open('Error loading lockers.', DialogType.Failure);
      },
    });
  }

  async onSubmit(): Promise<void> {
    if (this.formValid()) {
      const confirmed = await this.dialogService.confirm(
        'Are you sure you want to perform this action?',
        'Confirm Action'
      );

      if (confirmed) {
        const rent = {
          id: 0,
          idCustomer: this.newRent.idCustomer,
          idLocker: this.newRent.idLocker,
          rentalDate: this.newRent.rentalDate,
          returnDate: this.newRent.returnDate,
          userName: this.authService.getUserName(),
        };

        this.rentService.add(rent).subscribe({
          next: (data) => {
            this.dialogService.open(
              'Rent added successfully.',
              DialogType.Success
            );
            this.getLockerById(rent.idLocker);
          },
          error: (error) => {
            console.error('Error adding rent.', error);
            this.dialogService.open('Error adding rent.', DialogType.Failure);
          },
        });
      }
    } else {
      this.dialogService.open(
        'Please ensure all fields are filled correctly.',
        DialogType.Warning
      );
    }
  }

  getLockerById(id: number) {
    this.lockerService.getLockerById(id).subscribe({
      next: (data) => {
        this.locker = data;
        this.locker.rented = !this.locker.rented;
        this.updateLockerState();
      },
      error: (error) => {
        this.dialogService.open(
          'Error getting locker data.',
          DialogType.Failure
        );
      },
    });
  }

  updateLockerState() {
    this.lockerService.updateLockerRented(this.locker).subscribe({
      next: () => {
        console.log('State Rented for locker added.');
        this.getAllRents();
        this.getAllLockers();
      },
      error: (error) => {
        console.log('Error updating locker data.', error);
        this.dialogService.open(
          'Error updating locker data.',
          DialogType.Failure
        );
      },
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

    return (
      this.newRent.idCustomer != null &&
      this.newRent.idLocker != null &&
      rentalDay != null &&
      returnDay != null
    );
  }

  setReturnDay() {
    this.newRent.returnDate = this.commonService.getReturnDateString(
      this.newRent.rentalDate
    );
  }
}
