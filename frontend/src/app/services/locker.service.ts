import { Injectable } from '@angular/core';
import { LockerDTO } from '@models/locker-dto.models';
import { Locker } from '@models/locker.models';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { GLOBAL } from '@configuration/configuration.global';
import { EndpointType } from 'enums/endpoint-type.enum';

@Injectable({
  providedIn: 'root',
})
export class LockerService {
  constructor(private http: HttpClient) {}

  getAllLockers(): Observable<LockerDTO[]> {
    const url = `${GLOBAL.apiBaseUrl}/${EndpointType.Locker}/dto`;
    return this.http.get<LockerDTO[]>(url);
  }

  getLockerById(id: number): Observable<Locker> {
    const url = `${GLOBAL.apiBaseUrl}/${EndpointType.Locker}/${id}`;
    return this.http.get<Locker>(url);
  }

  addLocker(locker: Locker) {
    const url = `${GLOBAL.apiBaseUrl}/${EndpointType.Locker}`;
    return this.http.post(url, locker);
  }

  updateLocker(locker: Locker) {
    const url = `${GLOBAL.apiBaseUrl}/${EndpointType.Locker}`;
    return this.http.put(url, locker);
  }

  //Patch method
  updateLockerRented(locker: Locker) {
    const url = `${GLOBAL.apiBaseUrl}/${EndpointType.Locker}/${locker.id}`;
    return this.http.patch(url, locker);
  }

  deleteLocker(id: number) {
    const url = `${GLOBAL.apiBaseUrl}/${EndpointType.Locker}/${id}`;
    return this.http.delete<Locker>(url);
  }
}
