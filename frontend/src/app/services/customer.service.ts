import { Injectable } from '@angular/core';
import { Customer } from '../models/customer.models';
import { HttpClient } from '@angular/common/http';
import { CustomerDTO } from '../models/customer-dto.models';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  private BaseUrl: string = 'http://localhost:5184/api/customers';

  constructor(private http: HttpClient) { }

  getAllCustomers() {
    const url = `${this.BaseUrl}/dto`;
    return this.http.get<CustomerDTO[]>(url);
  }

  addCustomer(customer: Customer) {
    const url = `${this.BaseUrl}`;
    return this.http.post(url, customer);
  }

  getCustomerById(id: number) {
    const url = `${this.BaseUrl}/${id}`;
    return this.http.get<Customer>(url);
  }

  updateCustomer(customer: Customer) {
    const url = `${this.BaseUrl}/${customer.id}`;
    return this.http.put(url, customer);
  }

  deleteCustomer(id: number) {
    const url = `${this.BaseUrl}/${id}`;
    return this.http.delete(url);
  }
}
