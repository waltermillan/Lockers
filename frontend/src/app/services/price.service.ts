import { Injectable } from '@angular/core';
import { Price } from '../models/price.models';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PriceService {

  price?:Price;

  constructor(private http: HttpClient) { }

  getAllPrices(){
    const url = `http://localhost:5184/api/prices`;
    return this.http.get<Price[]>(url);
  }

  addPrice(price:Price){
    const url = `http://localhost:5184/api/prices`;
    return this.http.post<Price[]>(url,price);
  }

  updatePrice(price:Price){
    const url = `http://localhost:5184/api/prices/` + price.id;
    return this.http.put(url,price);
  }

  deletePrice(id:number){
    const url = `http://localhost:5184/api/prices/` + id;
    return this.http.delete<Price[]>(url);
  }
}
