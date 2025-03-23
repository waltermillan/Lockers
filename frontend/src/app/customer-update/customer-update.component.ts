import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { Customer } from '../models/customer.models';
import { CustomerService } from '../services/customer.service';

@Component({
  selector: 'app-customer-update',
  templateUrl: './customer-update.component.html',
  styleUrl: './customer-update.component.css'
})
export class CustomerUpdateComponent implements OnInit {
  @Input() customer: Customer | null = null; // Receives the customer from the parent
  @Output() close = new EventEmitter<void>();

  updCustomer:Customer = {
    id: 0,
    name: '',
    phone: '',
    document: '',
    address: '',	
	  idDocument: 0
  }

  customers: Customer[] = [];

  constructor(private customerService: CustomerService) 
  {
  }

  ngOnInit(): void {
    this.getCustomerById();
    this.getAllCustomers();
  }

  onSubmit(){
    this.updateCustomer();
  }

  getCustomerById()
  {
    if(this.customer){
      this.updCustomer.id = this.customer.id;
      this.updCustomer.name = this.customer.name;
      this.updCustomer.phone = this.customer.phone;
      this.updCustomer.document = this.customer.document;
      this.updCustomer.address = this.customer.address;
      this.updCustomer.idDocument = this.customer.idDocument;
    }
  }

  getAllCustomers():void{
    this.customerService.getAllCustomers().subscribe({
      next: (data) => {
        this.customers = data;
      },
      error: (error) => {
        console.error('Error loading customers.');
      }
    });
  }

  updateCustomer() {
    console.log('Updating customer', this.customer);
    this.customerService.updateCustomer(this.updCustomer).subscribe({
      next: (data) => {
        alert('Customer updated successfully.');
        console.log('Customer updated successfully.');
      },
      error: (error) => {
        if(error.status === 400)
          alert('the customer already exists!');
        console.error('Error updating new customer', error);
      }
    })
  }

  closePopup() {
    this.close.emit();
  }
}
