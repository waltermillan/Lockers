import { Locker } from './locker.models';
export class LockerDTO {
    id:number = 0;
    serialNumber:number = 0;
    idLocation:number = 0;
    location:string = '';
    price:number = 0;
    idPrice:number = 0;
    rented:boolean = false;
}
