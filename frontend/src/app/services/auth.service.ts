import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  public loggedIn: boolean = false; // Estado de autenticación

  constructor(private http: HttpClient) { }

  // Verificar si el usuario se autenticó correctamente
  login(usr:string, pwd: string){
    const url = `http://localhost:5184/api/administrators/login`;
    const params = new HttpParams().set('userName', usr).set('password', pwd)
    return this.http.post(url, null, {params});
  }

  // Verificar si está autenticado
  isAuthenticated(): boolean {
    return this.loggedIn;
  }

  // Método de logout
  logout() {
    this.loggedIn = false;
  }
}
