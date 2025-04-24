import { Injectable } from '@angular/core';
import { User } from '@models/user.model';
import { HttpClient } from '@angular/common/http';
import { GLOBAL } from '../configuration/configuration.global';
import { EndpointType } from 'enums/endpoint-type.enum';

@Injectable({
  providedIn: 'root',
})
export class UserServiceService {
  private endpoint: string = 'users';

  constructor(private http: HttpClient) {}

  add(user: User) {
    const url = `${GLOBAL.apiBaseUrl}/${EndpointType.User}`;
    return this.http.post(url, user);
  }

  getAll() {
    const url = `${GLOBAL.apiBaseUrl}/${EndpointType.User}`;
    return this.http.get<User[]>(url);
  }

  getById(id: number) {
    const url = `${GLOBAL.apiBaseUrl}/${EndpointType.User}/${id}`;
    return this.http.get<User>(url);
  }

  update(user: User) {
    const url = `${GLOBAL.apiBaseUrl}/${EndpointType.User}/${user.id}`;
    return this.http.put(url, user);
  }

  delete(id: number) {
    const url = `${GLOBAL.apiBaseUrl}/${EndpointType.User}/${id}`;
    return this.http.delete(url);
  }
}
