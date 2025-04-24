import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { Customer } from '@models/customer.models';
import { CustomerService } from '@services/customer.service';
import { Document } from '@models/document.models';
import { DocumentService } from '@services/document.service';

import { DialogService } from '@services/dialog-service.service';
import { DialogType } from 'enums/dialog-type.enum';

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

  documents:Document[] = []
  customers: Customer[] = [];

  constructor(private customerService: CustomerService,
              private documentService: DocumentService,
              private dialogService: DialogService){
  }

  ngOnInit(): void {
    this.getCustomerById();
    this.getAllCustomers();
    this.getAllDocuments();
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
    this.customerService.getAll().subscribe({
      next: (data) => {
        this.customers = data;
      },
      error: (error) => {
        console.error('Error loading customers.');
        this.dialogService.open('Error loading customers.', DialogType.Failure);
      }
    });
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

  updateCustomer() {
    console.log('Updating customer', this.customer);
    this.customerService.update(this.updCustomer).subscribe({
      next: (data) => {
        this.dialogService.open('Customer updated successfully.', DialogType.Success);
        console.log('Customer updated successfully.');
      },
      error: (error) => {
        if(error.status === 400)
          this.dialogService.open('The customer already exists.', DialogType.Warning);
        else
        {
          this.dialogService.open('Error updating new customer.', DialogType.Failure);
          console.error('Error updating new customer.', error);
        }
      }
    })
  }

  closePopup() {
    this.close.emit();
  }
}
