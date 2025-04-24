import { Injectable } from '@angular/core';
import jsPDF from 'jspdf';
import autoTable from 'jspdf-autotable';
import { GLOBAL } from '@configuration/configuration.global';
import { CommonService } from './common.service';

@Injectable({
  providedIn: 'root',
})
export class ExportService {
  constructor(private common: CommonService) {}

  exportHtmlTableByIdToPDF(tableId: string) {
    const fileName = GLOBAL.fileName.replace(
      'DATE',
      this.common.getCompleteFormattedDate
    );
    const tabla = document.getElementById(tableId);
    if (!tabla) {
      console.error(`No se encontró una tabla con el ID: ${tableId}`);
      return;
    }

    let headers: string[] = [];
    const rows: string[][] = [];
    let actionsColumnIndex = -1;

    const thead = tabla.querySelector('thead');
    if (thead) {
      const headerCells = thead.querySelectorAll('th');
      headerCells.forEach((cell, index) => {
        const text = cell.textContent?.trim() || '';
        if (text.toLowerCase() === 'actions') {
          actionsColumnIndex = index;
        } else {
          headers.push(text);
        }
      });
    }

    const tbody = tabla.querySelector('tbody');
    if (tbody) {
      const rowElements = tbody.querySelectorAll('tr');
      rowElements.forEach((row) => {
        const rowData: string[] = [];
        row.querySelectorAll('td').forEach((cell, index) => {
          if (index !== actionsColumnIndex) {
            rowData.push(cell.textContent?.trim() || '');
          }
        });
        rows.push(rowData);
      });
    }

    const doc = new jsPDF();
    autoTable(doc, {
      head: [headers],
      body: rows,
      styles: { fontSize: 10 },
      margin: { top: 20 },
    });

    doc.save(fileName);
  }
}
