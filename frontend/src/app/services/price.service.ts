import { Injectable } from '@angular/core';
import { Price } from '@models/price.models';
import { HttpClient } from '@angular/common/http';
import { GLOBAL } from '@configuration/configuration.global';
import { EndpointType } from 'enums/endpoint-type.enum';

@Injectable({
  providedIn: 'root',
})
export class PriceService {
  price?: Price;

  constructor(private http: HttpClient) {}

  getAll() {
    const url = `${GLOBAL.apiBaseUrl}/${EndpointType.Price}`;
    return this.http.get<Price[]>(url);
  }

  add(price: Price) {
    const url = `${GLOBAL.apiBaseUrl}/${EndpointType.Price}`;
    return this.http.post<Price[]>(url, price);
  }

  update(price: Price) {
    const url = `${GLOBAL.apiBaseUrl}/${EndpointType.Price}`;
    return this.http.put(url, price);
  }

  delete(id: number) {
    const url = `${GLOBAL.apiBaseUrl}/${EndpointType.Price}/${id}`;
    return this.http.delete<Price[]>(url);
  }
}
