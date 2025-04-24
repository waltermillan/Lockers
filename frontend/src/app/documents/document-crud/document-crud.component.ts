import { Component, OnInit } from '@angular/core';
import { Document } from '@models/document.models';
import { DocumentService } from '@services/document.service';
import { DialogService } from '@services/dialog-service.service';
import { DialogType } from 'enums/dialog-type.enum';

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

  constructor(private documentService: DocumentService,
              private dialogService:DialogService) {

  }

  ngOnInit(): void {
    this.getAllDocuments();
  }

  onSubmit(){
    console.log(JSON.stringify(this.newDocument));
    this.addDocument();
  }

  addDocument(){
      this.documentService.add(this.newDocument).subscribe({
        next: (data) => {
          this.getAllDocuments();
          this.dialogService.open('Document addedd successfully.', DialogType.Success);
          console.log('Document addedd successfully.');
        },
        error: (error) => {
          console.error('Error adding new document.',error)
          this.dialogService.open('Error adding new document.', DialogType.Failure);
        }
      });
  }

  getAllDocuments(){
    this.documentService.getAll().subscribe({
      next: (data) => {
        this.documents = data;
        console.log('Loading all documents.');
      },
      error: (error) => {
        console.error('Error adding new document.',error)
        this.dialogService.open('Error adding new document.', DialogType.Failure);
      }
    });
  }

  deleteDocument(id:number){
    this.documentService.delete(id).subscribe({
      next: (data) => {
        this.dialogService.open('Document deleted successfully.', DialogType.Success);
        console.log('Document deleted successfully.');
        this.getAllDocuments();
      },
      error: (error) => {
        console.error('Error deleting document.', error);
        this.dialogService.open('Error deleting document.', DialogType.Failure);
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
