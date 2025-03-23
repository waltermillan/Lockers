import { Injectable } from '@angular/core';
import { Role } from '../models/role.models';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  constructor(private http: HttpClient) { }


  addRole(role:Role){
    const url = `http://localhost:5184/api/roles`
    return this.http.post(url,role);
  }

  getAllRoles(){
    const url = `http://localhost:5184/api/roles`
    return this.http.get<Role[]>(url);
  }

  getRoleById(id:number){
    const url = `http://localhost:5184/api/roles` + id
    return this.http.get<Role>(url);
  }

  updateRole(role:Role){
    const url = `http://localhost:5184/api/roles` + role.id
    return this.http.put(url, role);
  }

  deleteteRole(id:number){
    const url = `http://localhost:5184/api/roles` + id
    return this.http.delete(url);
  }
}
