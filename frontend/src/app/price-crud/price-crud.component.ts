import { Component, OnInit } from '@angular/core';
import { Price } from '../models/price.models';
import { PriceService } from '../services/price.service';
import { error } from 'console';

@Component({
  selector: 'app-price-crud',
  templateUrl: './price-crud.component.html',
  styleUrl: './price-crud.component.css'
})
export class PriceCrudComponent implements OnInit{

  prices:Price[] = [];
  isPopupOpen:boolean = false;
  selectedPrice: Price | null = null;

  newPrice:Price = {
    id: 0,
    value: 0
  }

  constructor(private priceService: PriceService) {

  }

  ngOnInit(): void {
    this.getAllPrices();
  }

  onSubmit(){
    console.log(JSON.stringify(this.newPrice));
    this.addPrice();
  }

  addPrice(){
      this.priceService.addPrice(this.newPrice).subscribe({
        next: (data) => {
          this.getAllPrices();
          alert('Price addedd successfully.');
          console.log('Price addedd successfully.');
        },
        error: (error) => {
          console.error('Error adding new price.',error)
        }
      })
  }

  getAllPrices(){
    this.priceService.getAllPrices().subscribe({
      next: (data) => {
        this.prices = data;
        console.log('Loading all prices.');
      },
      error: (error) => {
        console.error('Error adding new price.',error)
      }
    });
  }

  deletePrice(id:number){
    this.priceService.deletePrice(id).subscribe({
      next: (data) => {
        alert('Price deleted successfully.');
        console.log('Price deleted successfully.');
        this.getAllPrices();
      },
      error: (error) => {
        console.error('Error deleting price', error);
      }
    })
  }

  updatePrice(price: Price) {
    this.selectedPrice = price;
    this.isPopupOpen = true;
  }

  closePopup() {
    this.isPopupOpen = false;
    this.selectedPrice = null;
    this.getAllPrices();
  }
}
