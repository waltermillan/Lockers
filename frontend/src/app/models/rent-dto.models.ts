export class RentDTO {
    id:number = 0;
    idCustomer:number = 0;
    customer:string = '';
    idLocker:number = 0;
    locker:string = '';
    rentalDate:Date = new Date();
    returnDate:string = '';//Date = new Date();
    userName?:string = '';
}
