import { Injectable } from '@angular/core';
import { LockerDTO } from '../models/locker-dto.models';
import { Locker } from '../models/locker.models';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class LockerService {

  private baseUrl = 'http://localhost:5184/api/lockers';

  constructor(private http: HttpClient) { }

  getAllLockers(): Observable<LockerDTO[]> {
    const url = `${this.baseUrl}/dto`;
    return this.http.get<LockerDTO[]>(url);
  }

  getLockerById(id: number): Observable<Locker> {
    const url = `${this.baseUrl}/${id}`;
    return this.http.get<Locker>(url);
  }

  addLocker(locker: Locker) {
    const url = `${this.baseUrl}`;
    return this.http.post(url, locker);
  }

  updateLocker(locker: Locker) {
    const url = `${this.baseUrl}/${locker.id}`;
    return this.http.put(url, locker);
  }

  //Patch method
  updateLockerRented(locker: Locker) {
    const url = `${this.baseUrl}/${locker.id}`;
    return this.http.patch(url, locker);
  }

  deleteLocker(id: number) {
    const url = `${this.baseUrl}/${id}`;
    return this.http.delete<Locker>(url);
  }
}
