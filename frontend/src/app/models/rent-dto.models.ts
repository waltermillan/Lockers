export class RentDTO {
  id: number;
  idCustomer: number;
  customer: string;
  idLocker: number;
  locker: string;
  rentalDate: Date;
  returnDate: string;
  userName?: string;

  constructor() {
    this.id = 0;
    this.idCustomer = 0;
    this.customer = '';
    this.idLocker = 0;
    this.locker = '';
    this.rentalDate = new Date();
    this.returnDate = '';
    this.userName = '';
  }
}
