import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private BaseUrl: string = 'http://localhost:5184/api/users';

  public loggedIn: boolean = false; 
  public userLogged: string = ''; 

  constructor(private http: HttpClient) { }

  login(usr: string, pwd: string) {
    const url = `${this.BaseUrl}/login`;
    const params = new HttpParams().set('userName', usr).set('password', pwd);
    return this.http.post(url, null, { params });
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
