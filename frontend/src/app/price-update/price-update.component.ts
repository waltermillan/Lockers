import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { Price } from '../models/price.models';
import { PriceService } from '../services/price.service';


@Component({
  selector: 'app-price-update',
  templateUrl: './price-update.component.html',
  styleUrl: './price-update.component.css'
})
export class PriceUpdateComponent implements OnInit {
  @Input() price: Price | null = null; // Recived Prices from father (lprice-crud)
  @Output() close = new EventEmitter<void>();

   updPrice:Price = {
      id: 0,
      value: 0
    }

    prices: Price[] = [];

    constructor(private priceService: PriceService) 
{
}

ngOnInit(): void {
  this.getPriceById();
  this.getAllPrices();
}


onSubmit(){
  this.updatePrice();
}

getPriceById()
{
  if(this.price){
    this.updPrice.id = this.price.id;
    this.updPrice.value = this.price.value;
  }
}

getAllPrices():void{
  this.priceService.getAllPrices().subscribe({
    next: (data) => {
      this.prices = data;
    },
    error: (error) => {
      console.error('Error loading prices.');
    }
  });
}

updatePrice() {
    console.log('Updating price', this.price);
    this.priceService.updatePrice(this.updPrice).subscribe({
      next: (data) => {
        alert('Price updated successfully.');
        console.log('price updated successfully.');
      },
      error: (error) => {
        if(error.status === 400)
          alert('the price already exists!');
        console.error('Error updating new price', error);
      }
    })
  }

  closePopup() {
    this.close.emit();
  }
}
