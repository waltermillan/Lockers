import { Injectable } from '@angular/core';
import { Customer } from '../models/customer.models';
import { HttpClient } from '@angular/common/http';
import { CustomerDTO } from '../models/customer-dto.models';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private http: HttpClient) { }

  getAllCustomers(){
    const url = `http://localhost:5184/api/customers/dto`
    return this.http.get<CustomerDTO[]>(url);
  }

  addCustomer(customer:Customer){
    const url = `http://localhost:5184/api/customers`
    return this.http.post(url,customer);
  }

  getCustomerById(id:number){
    const url = `http://localhost:5184/api/customers/` + id
    return this.http.get<Customer>(url);
  }

  updateCustomer(customer:Customer){
    const url = `http://localhost:5184/api/customers/` + customer.id
    return this.http.put(url, customer);
  }

  deleteteCustomer(id:number){
    const url = `http://localhost:5184/api/Customers/` + id
    return this.http.delete(url);
  }
}
