import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, HttpClientModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
  documents: any[] = [];

  constructor(private apiService: ApiService) {}
  ngOnInit(): void {
    this.apiService.getDocuments().subscribe({
      next: (data) => {
        this.documents = data;
      },
      error: (error) => {
        console.error('Failed to fetch documents:', error);
      }
    });
  }
}