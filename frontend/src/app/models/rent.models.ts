export class Rent {
  id: number;
  idCustomer: number;
  idLocker: number;
  rentalDate: Date;
  returnDate: string;
  userName?: string | null;

  constructor() {
    this.id = 0;
    this.idCustomer = 0;
    this.idLocker = 0;
    this.rentalDate = new Date();
    this.returnDate = '';
    this.userName = '';
  }
}
