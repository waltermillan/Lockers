import { Injectable } from '@angular/core';
import { Location } from '../models/location.models';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class LocationService {

  private BaseUrl: string = 'http://localhost:5184/api/locations';

  constructor(private http: HttpClient) { }

  getAllLocations() {
    const url = `${this.BaseUrl}`;
    return this.http.get<Location[]>(url);
  }

  addLocation(location: Location) {
    const url = `${this.BaseUrl}`;
    return this.http.post<Location[]>(url, location);
  }

  updateLocation(location: Location) {
    const url = `${this.BaseUrl}/${location.id}`;
    return this.http.put(url, location);
  }

  deleteLocation(id: number) {
    const url = `${this.BaseUrl}/${id}`;
    return this.http.delete<Location[]>(url);
  }
}
