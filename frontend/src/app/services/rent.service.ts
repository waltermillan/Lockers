import { Injectable } from '@angular/core';
import { Rent } from '../models/rent.models';
import { RentDTO } from '../models/rent-dto.models';
import { HttpClient } from '@angular/common/http';

import { Locker  } from '../models/locker.models';
import { LockerService } from './locker.service';

import { Observable } from 'rxjs';
import { error } from 'console';

@Injectable({
  providedIn: 'root'
})
export class RentService {

  locker:Locker = new Locker();

  constructor(private http: HttpClient,
              private lockerService: LockerService) { }

  getAllRents() : Observable<RentDTO[]>{
    const url = `http://localhost:5184/api/rents/dto`;
    return this.http.get<RentDTO[]>(url);
  }

  addRent(rent:Rent){
    //console.log(JSON.stringify(rent));
    const url = `http://localhost:5184/api/rents`;
    return this.http.post(url, rent) 
  }

  updateRent(rent:Rent){
    const url = `http://localhost:5184/api/rents/` + rent.id;
    return this.http.put(url, rent) 
  }

  deleteRent(id:number){
    const url = `http://localhost:5184/api/rents/` + id;
    return this.http.delete<Rent>(url);
  }
}
