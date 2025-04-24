import { Injectable } from '@angular/core';
import { Location } from '@models/location.models';
import { HttpClient } from '@angular/common/http';
import { GLOBAL } from '@configuration/configuration.global';
import { EndpointType } from 'enums/endpoint-type.enum';

@Injectable({
  providedIn: 'root',
})
export class LocationService {
  constructor(private http: HttpClient) {}

  getAll() {
    const url = `${GLOBAL.apiBaseUrl}/${EndpointType.Location}`;
    return this.http.get<Location[]>(url);
  }

  add(location: Location) {
    const url = `${GLOBAL.apiBaseUrl}/${EndpointType.Location}`;
    return this.http.post<Location[]>(url, location);
  }

  update(location: Location) {
    const url = `${GLOBAL.apiBaseUrl}/${EndpointType.Location}/${location.id}`;
    return this.http.put(url, location);
  }

  delete(id: number) {
    const url = `${GLOBAL.apiBaseUrl}/${EndpointType.Location}/${id}`;
    return this.http.delete<Location[]>(url);
  }
}
