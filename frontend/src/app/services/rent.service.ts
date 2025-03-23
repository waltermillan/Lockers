import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Rent } from '../models/rent.models';
import { RentDTO } from '../models/rent-dto.models';
import { Locker } from '../models/locker.models';

@Injectable({
  providedIn: 'root'
})
export class RentService {

  private BaseUrl: string = 'http://localhost:5184/api/rents';

  locker: Locker = new Locker();

  constructor(private http: HttpClient) { }

  getAllRents(): Observable<RentDTO[]> {
    const url = `${this.BaseUrl}/dto`;
    return this.http.get<RentDTO[]>(url);
  }

  addRent(rent: Rent) {
    const url = `${this.BaseUrl}`;
    return this.http.post(url, rent);
  }

  updateRent(rent: Rent) {
    const url = `${this.BaseUrl}/${rent.id}`;
    return this.http.put(url, rent);
  }

  deleteRent(id: number) {
    const url = `${this.BaseUrl}/${id}`;
    return this.http.delete<Rent>(url);
  }
}
