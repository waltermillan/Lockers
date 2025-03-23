import { Injectable } from '@angular/core';
import { Role } from '../models/role.models';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  private BaseUrl: string = 'http://localhost:5184/api/roles';

  constructor(private http: HttpClient) { }

  addRole(role: Role) {
    const url = `${this.BaseUrl}`;
    return this.http.post(url, role);
  }

  getAllRoles() {
    const url = `${this.BaseUrl}`;
    return this.http.get<Role[]>(url);
  }

  getRoleById(id: number) {
    const url = `${this.BaseUrl}/${id}`;
    return this.http.get<Role>(url);
  }

  updateRole(role: Role) {
    const url = `${this.BaseUrl}/${role.id}`;
    return this.http.put(url, role);
  }

  deleteRole(id: number) {
    const url = `${this.BaseUrl}/${id}`;
    return this.http.delete(url);
  }
}
