import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { Document } from '@models/document.models';
import { DocumentService } from '@services/document.service';
import { DialogService } from '@services/dialog-service.service';
import { DialogType } from 'enums/dialog-type.enum';

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

  constructor(private documentService: DocumentService,
              private dialogService: DialogService){
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
    this.documentService.getAll().subscribe({
      next: (data) => {
        this.documents = data;
      },
      error: (error) => {
        console.error('Error loading documents.');
        this.dialogService.open('Error loading documents.', DialogType.Failure);
      }
    });
  }

  updateDocument() {
    console.log('Updating document', this.document);
    this.documentService.update(this.updDocument).subscribe({
      next: (data) => {
        this.dialogService.open('Document updated successfully.', DialogType.Success);
        console.log('document updated successfully.');
      },
      error: (error) => {
        if(error.status === 400)
          this.dialogService.open('the document already exists!', DialogType.Warning);
        else{
          console.error('Error updating new document.', error);
          this.dialogService.open('Error updating new document.', DialogType.Failure);
        }
      }
    })
  }

  closePopup() {
    this.close.emit();
  }
}
