import { Injectable } from '@angular/core';
import { Role } from '@models/role.models';
import { HttpClient } from '@angular/common/http';
import { GLOBAL } from '@configuration/configuration.global';
import { EndpointType } from 'enums/endpoint-type.enum';

@Injectable({
  providedIn: 'root',
})
export class RoleService {
  constructor(private http: HttpClient) {}

  add(role: Role) {
    const url = `${GLOBAL.apiBaseUrl}/${EndpointType.Role}`;
    return this.http.post(url, role);
  }

  getAll() {
    const url = `${GLOBAL.apiBaseUrl}/${EndpointType.Role}`;
    return this.http.get<Role[]>(url);
  }

  getById(id: number) {
    const url = `${GLOBAL.apiBaseUrl}/${EndpointType.Role}/${id}`;
    return this.http.get<Role>(url);
  }

  update(role: Role) {
    const url = `${GLOBAL.apiBaseUrl}/${EndpointType.Role}`;
    return this.http.put(url, role);
  }

  delete(id: number) {
    const url = `${GLOBAL.apiBaseUrl}/${EndpointType.Role}/${id}`;
    return this.http.delete(url);
  }
}
