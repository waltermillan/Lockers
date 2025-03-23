import { Injectable } from '@angular/core';
import { Price } from '../models/price.models';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PriceService {

  private BaseUrl: string = 'http://localhost:5184/api/prices';

  price?: Price;

  constructor(private http: HttpClient) { }

  getAllPrices() {
    const url = `${this.BaseUrl}`;
    return this.http.get<Price[]>(url);
  }

  addPrice(price: Price) {
    const url = `${this.BaseUrl}`;
    return this.http.post<Price[]>(url, price);
  }

  updatePrice(price: Price) {
    const url = `${this.BaseUrl}/${price.id}`;
    return this.http.put(url, price);
  }

  deletePrice(id: number) {
    const url = `${this.BaseUrl}/${id}`;
    return this.http.delete<Price[]>(url);
  }
}
