import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { Price } from '@models/price.models';
import { PriceService } from '@services/price.service';
import { DialogService } from '@services/dialog-service.service';
import { DialogType } from 'enums/dialog-type.enum';

@Component({
  selector: 'app-price-update',
  templateUrl: './price-update.component.html',
  styleUrl: './price-update.component.css',
})
export class PriceUpdateComponent implements OnInit {
  @Input() price: Price | null = null; // Recived Prices from father (lprice-crud)
  @Output() close = new EventEmitter<void>();

  updPrice: Price = {
    id: 0,
    value: 0,
  };

  prices: Price[] = [];

  constructor(
    private priceService: PriceService,
    private dialogService: DialogService
  ) {}

  ngOnInit(): void {
    this.getPriceById();
    this.getAllPrices();
  }

  onSubmit() {
    this.updatePrice();
  }

  getPriceById() {
    if (this.price) {
      this.updPrice.id = this.price.id;
      this.updPrice.value = this.price.value;
    }
  }

  getAllPrices(): void {
    this.priceService.getAll().subscribe({
      next: (data) => {
        this.prices = data;
      },
      error: (error) => {
        console.error('Error loading prices.');
        this.dialogService.open('Error loading prices.', DialogType.Failure);
      },
    });
  }

  updatePrice() {
    console.log('Updating price', this.price);
    this.priceService.update(this.updPrice).subscribe({
      next: (data) => {
        this.dialogService.open(
          'Price updated successfully.',
          DialogType.Success
        );
        console.log('price updated successfully.');
      },
      error: (error) => {
        if (error.status === 400)
          this.dialogService.open(
            'the price already exists!',
            DialogType.Warning
          );
        else {
          console.error('Error updating new price.', error);
          this.dialogService.open(
            'Error updating new price.',
            DialogType.Failure
          );
        }
      },
    });
  }

  closePopup() {
    this.close.emit();
  }
}
