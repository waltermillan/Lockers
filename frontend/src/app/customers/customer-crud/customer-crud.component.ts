import { Component, OnInit } from '@angular/core';
import { Customer } from '@models/customer.models';
import { CustomerDTO } from '@models/customer-dto.models';;
import { CustomerService } from '@services/customer.service';
import { Document } from '@models/document.models';
import { DocumentService } from '@services/document.service';
import { DialogService } from '@services/dialog-service.service';
import { DialogType } from 'enums/dialog-type.enum';

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
              private documentService: DocumentService,
              private dialogService: DialogService) {

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
    this.documentService.getAll().subscribe({
      next: (data) => {
        this.documents = data;
        console.log('Loanding documents successfully');
      },
      error: (error) => {
        console.error('Error loanding documents.', error);
        this.dialogService.open('Error loanding documents.', DialogType.Failure);
      }
    });
  }

  addCustomer(){
      this.customerService.add(this.newCustomer).subscribe({
        next: (data) => {
          this.getAllCustomers();
          this.dialogService.open('Customer addedd successfully.', DialogType.Success);
          console.log('Customer addedd successfully.');
        },
        error: (error) => {
          console.error('Error adding new customer.',error);
          this.dialogService.open('Error adding new customer.', DialogType.Failure);
        }
      });
  }

  getAllCustomers(){
    this.customerService.getAll().subscribe({
      next: (data) => {
        this.customers = data;
        console.log('Loading all customers.');
      },
      error: (error) => {
        console.error('Error getting customers.',error)
        this.dialogService.open('Error getting customers.', DialogType.Failure);
      }
    });
  }

  deleteCustomer(id:number){
    this.customerService.delete(id).subscribe({
      next: (data) => {
        this.dialogService.open('Customer deleted successfully.', DialogType.Success);
        console.log('Customer deleted successfully.');
        this.getAllCustomers();
      },
      error: (error) => {
        console.error('Error deleting customer.', error);
        this.dialogService.open('Error deleting customer.', DialogType.Failure);
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
