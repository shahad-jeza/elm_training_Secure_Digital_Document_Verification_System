import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl = 'http://localhost:5233/api';

  constructor(private http: HttpClient) {}

  // Fetch all documents
  getDocuments(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/Documents`);
  }

  // Upload a document
  uploadDocument(document: FormData): Observable<any> {
    return this.http.post(`${this.baseUrl}/Documents`, document);
  }

  // Verify a document
  verifyDocument(verificationData: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/verify`, verificationData);
  }
}