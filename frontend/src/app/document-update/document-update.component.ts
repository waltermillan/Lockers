import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { Document } from '../models/document.models';
import { DocumentService } from '../services/document.service';

@Component({
  selector: 'app-document-update',
  templateUrl: './document-update.component.html',
  styleUrl: './document-update.component.css'
})
export class DocumentUpdateComponent implements OnInit {
  @Input() document: Document | null = null; // Receives the document from the parent
  @Output() close = new EventEmitter<void>();

  updDocument:Document = {
    id: 0,
    description: '',
  }

  documents: Document[] = [];

  constructor(private documentService: DocumentService) 
  {
  }

  ngOnInit(): void {
    this.getDocumentById();
    this.getAllDocuments();
  }

  onSubmit(){
    this.updateDocument();
  }

  getDocumentById()
  {
    if(this.document){
      this.updDocument.id = this.document.id;
      this.updDocument.description = this.document.description;
    }
  }

  getAllDocuments():void{
    this.documentService.getAllDocuments().subscribe({
      next: (data) => {
        this.documents = data;
      },
      error: (error) => {
        console.error('Error loading documents.');
      }
    });
  }

  updateDocument() {
    console.log('Updating document', this.document);
    this.documentService.updateDocument(this.updDocument).subscribe({
      next: (data) => {
        alert('Document updated successfully.');
        console.log('document updated successfully.');
      },
      error: (error) => {
        if(error.status === 400)
          alert('the document already exists!');
        console.error('Error updating new document', error);
      }
    })
  }

  closePopup() {
    this.close.emit();
  }
}
