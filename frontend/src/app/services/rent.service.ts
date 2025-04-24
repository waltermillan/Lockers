import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Rent } from '@models/rent.models';
import { RentDTO } from '@models/rent-dto.models';
import { Locker } from '@models/locker.models';
import { GLOBAL } from '@configuration/configuration.global';
import { EndpointType } from 'enums/endpoint-type.enum';

@Injectable({
  providedIn: 'root',
})
export class RentService {
  locker: Locker = new Locker();

  constructor(private http: HttpClient) {}

  getAll(): Observable<RentDTO[]> {
    const url = `${GLOBAL.apiBaseUrl}/${EndpointType.Rent}/dto`;
    return this.http.get<RentDTO[]>(url);
  }

  add(rent: Rent) {
    const url = `${GLOBAL.apiBaseUrl}/${EndpointType.Rent}`;
    return this.http.post(url, rent);
  }

  update(rent: Rent) {
    const url = `${GLOBAL.apiBaseUrl}/${EndpointType.Rent}/${rent.id}`;
    return this.http.put(url, rent);
  }

  delete(id: number) {
    const url = `${GLOBAL.apiBaseUrl}/${EndpointType.Rent}/${id}`;
    return this.http.delete<Rent>(url);
  }
}
