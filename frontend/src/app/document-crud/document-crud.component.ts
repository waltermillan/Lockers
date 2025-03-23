import { Component, OnInit } from '@angular/core';
import { Document } from '../models/document.models';
import { DocumentService } from '../services/document.service';

@Component({
  selector: 'app-document-crud',
  templateUrl: './document-crud.component.html',
  styleUrl: './document-crud.component.css'
})
export class DocumentCrudComponent implements OnInit{

  documents:Document[] = [];
  isPopupOpen:boolean = false;
  selectedDocument: Document | null = null;

  newDocument:Document = {
    id: 0,
    description: '',
  }

  constructor(private documentService: DocumentService) {

  }

  ngOnInit(): void {
    this.getAllDocuments();
  }

  onSubmit(){
    console.log(JSON.stringify(this.newDocument));
    this.addDocument();
  }

  addDocument(){
      this.documentService.addDocument(this.newDocument).subscribe({
        next: (data) => {
          this.getAllDocuments();
          alert('Document addedd successfully.');
          console.log('Document addedd successfully.');
        },
        error: (error) => {
          console.error('Error adding new document.',error)
        }
      });
  }

  getAllDocuments(){
    this.documentService.getAllDocuments().subscribe({
      next: (data) => {
        this.documents = data;
        console.log('Loading all documents.');
      },
      error: (error) => {
        console.error('Error adding new document.',error)
      }
    });
  }

  deleteDocument(id:number){
    this.documentService.deleteDocument(id).subscribe({
      next: (data) => {
        alert('Document deleted successfully.');
        console.log('Document deleted successfully.');
        this.getAllDocuments();
      },
      error: (error) => {
        console.error('Error deleting document', error);
      }
    });
  }

  updateDocument(document: Document) {
    this.selectedDocument = document;
    this.isPopupOpen = true;
  }

  closePopup() {
    this.isPopupOpen = false;
    this.selectedDocument = null;
    this.getAllDocuments();
  }
}
