import { Injectable } from '@angular/core';
import { Document } from '../models/document.models';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DocumentService {

  private BaseUrl: string = 'http://localhost:5184/api/documents';

  constructor(private http: HttpClient) { }

  getAllDocuments() {
    const url = `${this.BaseUrl}`;
    return this.http.get<Document[]>(url);
  }

  addDocument(document: Document) {
    const url = `${this.BaseUrl}`;
    return this.http.post<Document[]>(url, document);
  }

  updateDocument(document: Document) {
    const url = `${this.BaseUrl}/${document.id}`;
    return this.http.put(url, document);
  }

  deleteDocument(id: number) {
    const url = `${this.BaseUrl}/${id}`;
    return this.http.delete<Document[]>(url);
  }
}
