import { Component, OnInit } from '@angular/core';
import { Customer } from '../models/customer.models';
import { CustomerDTO } from '../models/customer-dto.models';
import { CustomerService } from '../services/customer.service';
import { Document } from '../models/document.models';
import { DocumentService } from '../services/document.service';

@Component({
  selector: 'app-customer-crud',
  templateUrl: './customer-crud.component.html',
  styleUrl: './customer-crud.component.css'
})
export class CustomerCrudComponent implements OnInit{

  customers:CustomerDTO[] = [];
  isPopupOpen:boolean = false;
  selectedCustomer: Customer | null = null;

  documents:Document[] = []
  newCustomer:CustomerDTO = {
    id: 0,
    name: '',
    phone: '',
    document: '',    
    address: '',
    idDocument: 0,
    typeDocument: ''
  }

  constructor(private customerService: CustomerService,
              private documentService: DocumentService
  ) {

  }

  ngOnInit(): void {
    this.getAllCustomers();
    this.getAllDocuments();
  }

  onSubmit(){
    console.log(JSON.stringify(this.newCustomer));
    this.addCustomer();
  }

  getAllDocuments(){
    this.documentService.getAllDocuments().subscribe({
      next: (data) => {
        this.documents = data;
        console.log('Loanding documents successfully');
      },
      error: (error) => {
        console.error('Error loanding documents.', error);
      }
    });
  }

  addCustomer(){
      this.customerService.addCustomer(this.newCustomer).subscribe({
        next: (data) => {
          this.getAllCustomers();
          alert('Customer addedd successfully.');
          console.log('Customer addedd successfully.');
        },
        error: (error) => {
          console.error('Error adding new customer.',error)
        }
      });
  }

  getAllCustomers(){
    this.customerService.getAllCustomers().subscribe({
      next: (data) => {
        this.customers = data;
        console.log('Loading all customers.');
      },
      error: (error) => {
        console.error('Error adding new customer.',error)
      }
    });
  }

  deleteCustomer(id:number){
    this.customerService.deleteteCustomer(id).subscribe({
      next: (data) => {
        alert('Customer deleted successfully.');
        console.log('Customer deleted successfully.');
        this.getAllCustomers();
      },
      error: (error) => {
        console.error('Error deleting customer', error);
      }
    });
  }

  updateCustomer(customerDTO: CustomerDTO) {

    const customer = new Customer()

    customer.id = customerDTO.id;
    customer.name = customerDTO.name;
    customer.phone = customerDTO.phone;
    customer.address = customerDTO.address;
    customer.document = customerDTO.document;
    customer.idDocument = customerDTO.idDocument;

    this.selectedCustomer = customer;
    this.isPopupOpen = true;
  }

  closePopup() {
    this.isPopupOpen = false;
    this.selectedCustomer = null;
    this.getAllCustomers();
  }
}
