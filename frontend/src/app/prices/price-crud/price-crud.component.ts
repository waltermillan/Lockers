import { Component, OnInit } from '@angular/core';
import { Price } from '@models/price.models';
import { PriceService } from '@services/price.service';
import { DialogService } from '@services/dialog-service.service';
import { DialogType } from 'enums/dialog-type.enum';

@Component({
  selector: 'app-price-crud',
  templateUrl: './price-crud.component.html',
  styleUrl: './price-crud.component.css',
})
export class PriceCrudComponent implements OnInit {
  prices: Price[] = [];
  isPopupOpen: boolean = false;
  selectedPrice: Price | null = null;

  newPrice: Price = {
    id: 0,
    value: 0,
  };

  constructor(
    private priceService: PriceService,
    private dialogService: DialogService
  ) {}

  ngOnInit(): void {
    this.getAllPrices();
  }

  onSubmit() {
    console.log(JSON.stringify(this.newPrice));
    this.addPrice();
  }

  addPrice() {
    this.priceService.add(this.newPrice).subscribe({
      next: (data) => {
        this.getAllPrices();
        this.dialogService.open(
          'Price addedd successfully.',
          DialogType.Success
        );
        console.log('Price addedd successfully.');
      },
      error: (error) => {
        console.error('Error adding new price.', error);
        this.dialogService.open('Error adding new price.', DialogType.Failure);
      },
    });
  }

  getAllPrices() {
    this.priceService.getAll().subscribe({
      next: (data) => {
        this.prices = data;
        console.log('Loading all prices.');
      },
      error: (error) => {
        console.error('Error loading prices.', error);
        this.dialogService.open('Error loading prices.', DialogType.Failure);
      },
    });
  }

  deletePrice(id: number) {
    this.priceService.delete(id).subscribe({
      next: (data) => {
        this.dialogService.open(
          'Price deleted successfully.',
          DialogType.Success
        );
        console.log('Price deleted successfully.');
        this.getAllPrices();
      },
      error: (error) => {
        console.error('Error deleting price.', error);
        this.dialogService.open('Error deleting price.', DialogType.Failure);
      },
    });
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
