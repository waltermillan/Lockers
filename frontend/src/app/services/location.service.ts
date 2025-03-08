import { Injectable } from '@angular/core';
import { Location } from '../models/location.models';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class LocationService {

  constructor(private http: HttpClient) { }

  getAllLocations(){
    const url = `http://localhost:5184/api/locations`;
    return this.http.get<Location[]>(url);
  }

  addLocation(location:Location){
    const url = `http://localhost:5184/api/locations`;
    return this.http.post<Location[]>(url,location);
  }

  updateLocation(location:Location){
    const url = `http://localhost:5184/api/locations/` + location.id;
    return this.http.put(url,location);
  }

  deleteLocation(id:number){
    const url = `http://localhost:5184/api/locations/` + id;
    return this.http.delete<Location[]>(url);
  }
}
