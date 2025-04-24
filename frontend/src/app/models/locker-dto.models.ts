export class LockerDTO {
  id: number;
  serialNumber: number;
  idLocation: number;
  location: string;
  price: number;
  idPrice: number;
  rented: boolean;

  constructor() {
    this.id = 0;
    this.serialNumber = 0;
    this.idLocation = 0;
    this.location = '';
    this.price = 0;
    this.idPrice = 0;
    this.rented = false;
  }
}
