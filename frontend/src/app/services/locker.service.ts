import { Injectable } from '@angular/core';
import { LockerDTO  } from '../models/locker-dto.models';
import { Locker  } from '../models/locker.models';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class LockerService {

  private baseUrl = 'http://localhost:5184/api/lockers';

  constructor(private http: HttpClient) { }

  getAllLockers() : Observable<LockerDTO[]>{
    const url = `http://localhost:5184/api/lockers/dto`;
    return this.http.get<LockerDTO[]>(url);
  }

  getLockerById(id: number): Observable<Locker> {
    const url = `${this.baseUrl}/${id}`;
    //alert('url: ' + url)
    return this.http.get<Locker>(url);
  }

  addLocker(locker:Locker){
    const url = `http://localhost:5184/api/lockers`;
    return this.http.post(url, locker) 
  }

  updateLocker(locker:Locker){
    const url = `http://localhost:5184/api/lockers/` + locker.id;
    return this.http.put(url, locker) 
  }

  //PATCH METHOD
  updateLockerRented(locker: Locker) {
    const url = `http://localhost:5184/api/lockers/` + locker.id;
    //alert('url (PATCH): ' + url);
    //alert('locker (POST-INVOKE): ' + JSON.stringify(locker));
    return this.http.patch(url, locker);
}


  deleteLocker(id:number){
    const url = `http://localhost:5184/api/lockers/` + id;
    return this.http.delete<Locker>(url);
  }
}