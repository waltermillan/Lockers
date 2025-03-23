import { Injectable } from '@angular/core';
import { DatePipe } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class CommonService {

  constructor(private datePipe: DatePipe) { }

  getCompleteFormattedDate(){
    const now = new Date();

    const year = now.getFullYear();
    const month = (now.getMonth() + 1).toString().padStart(2, '0');
    const day = now.getDate().toString().padStart(2, '0');
    const hours = now.getHours().toString().padStart(2, '0');
    const minutes = now.getMinutes().toString().padStart(2, '0');
    const seconds = now.getSeconds().toString().padStart(2, '0');

    return `${year}${month}${day}${hours}${minutes}${seconds}`;
   }

   getShortFormattedDate(date: Date, days: number) {
    const newDate = new Date(date);
    newDate.setDate(newDate.getDate() + days);
    const day = ("0" + newDate.getDate()).slice(-2);
    const month = ("0" + (newDate.getMonth() + 1)).slice(-2);
    const year = newDate.getFullYear();
    return `${day}/${month}/${year}`;
  }  

  getReturnDateString(rentalDate: Date): string {
    const rentalDateString = rentalDate.toString();
    let [sYear, sMonth, sDay] = rentalDateString.split('-');
    let year = parseInt(sYear, 10);
    let month = parseInt(sMonth, 10) - 1;
    let day = parseInt(sDay, 10) + 1;
    
    const daysInMonth = [31, (year % 4 === 0 && (year % 100 !== 0 || year % 400 === 0)) ? 29 : 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
    
    if (day > daysInMonth[month]) {
      day = 1;
      month++;
      if (month > 11) {
        month = 0;
        year++;
      }
    }
    
    return `${year}-${(month + 1).toString().padStart(2, '0')}-${day.toString().padStart(2, '0')}`;
  }
  
}
