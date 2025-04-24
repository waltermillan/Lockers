import { Injectable } from '@angular/core';
import { Customer } from '@models/customer.models';
import { HttpClient } from '@angular/common/http';
import { CustomerDTO } from '@models/customer-dto.models';

import { GLOBAL } from '@configuration/configuration.global';
import { EndpointType } from 'enums/endpoint-type.enum';

@Injectable({
  providedIn: 'root',
})
export class CustomerService {
  constructor(private http: HttpClient) {}

  getAll() {
    const url = `${GLOBAL.apiBaseUrl}/${EndpointType.Customer}/dto`;
    return this.http.get<CustomerDTO[]>(url);
  }

  add(customer: Customer) {
    const url = `${GLOBAL.apiBaseUrl}/${EndpointType.Customer}`;
    return this.http.post(url, customer);
  }

  getById(id: number) {
    const url = `${GLOBAL.apiBaseUrl}/${EndpointType.Customer}/${id}`;
    return this.http.get<Customer>(url);
  }

  update(customer: Customer) {
    const url = `${GLOBAL.apiBaseUrl}/${EndpointType.Customer}`;
    return this.http.put(url, customer);
  }

  delete(id: number) {
    const url = `${GLOBAL.apiBaseUrl}/${EndpointType.Customer}/${id}`;
    return this.http.delete(url);
  }
}
