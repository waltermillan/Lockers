export class CustomerDTO {
  id: number;
  name: string;
  phone: string;
  document: string;
  address: string;
  idDocument: number;
  typeDocument: string;

  constructor() {
    this.id = 0;
    this.name = '';
    this.phone = '';
    this.document = '';
    this.address = '';
    this.idDocument = 0;
    this.typeDocument = '';
  }
}
