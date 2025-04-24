export class Customer {
  id: number;
  name: string;
  phone: string;
  document: string;
  address: string;
  idDocument: number;

  constructor() {
    this.id = 0;
    this.name = '';
    this.phone = '';
    this.document = '';
    this.address = '';
    this.idDocument = 0;
  }
}
