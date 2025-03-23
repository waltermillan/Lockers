import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  public loggedIn: boolean = false; 
  public userLogged: string = ''; 

  constructor(private http: HttpClient) { }

  // Verificar si el usuario se autenticó correctamente
  login(usr:string, pwd: string){
    const url = `http://localhost:5184/api/users/login`;
    const params = new HttpParams().set('userName', usr).set('password', pwd)
    return this.http.post(url, null, {params});
  }

  getUserLogged(): string {
    return this.userLogged;
  }

  isAuthenticated(): boolean {
    return this.loggedIn;
  }

  logout() {
    this.loggedIn = false;
  }
}
