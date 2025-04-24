import { Injectable } from '@angular/core';
import { Document } from '@models/document.models';
import { HttpClient } from '@angular/common/http';
import { GLOBAL } from '@configuration/configuration.global';
import { EndpointType } from 'enums/endpoint-type.enum';

@Injectable({
  providedIn: 'root',
})
export class DocumentService {
  constructor(private http: HttpClient) {}

  getAll() {
    const url = `${GLOBAL.apiBaseUrl}/${EndpointType.Document}`;
    return this.http.get<Document[]>(url);
  }

  add(document: Document) {
    const url = `${GLOBAL.apiBaseUrl}/${EndpointType.Document}`;
    return this.http.post<Document[]>(url, document);
  }

  update(document: Document) {
    const url = `${GLOBAL.apiBaseUrl}/${EndpointType.Document}`;
    return this.http.put(url, document);
  }

  delete(id: number) {
    const url = `${GLOBAL.apiBaseUrl}/${EndpointType.Document}/${id}`;
    return this.http.delete<Document[]>(url);
  }
}
