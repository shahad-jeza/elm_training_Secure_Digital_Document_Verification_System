import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ApiService } from '../services/api.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-document-upload',
  standalone: true, // Mark as standalone
  imports: [ReactiveFormsModule, CommonModule], // Add ReactiveFormsModule and CommonModule
  templateUrl: './document-upload.component.html',
  styleUrls: ['./document-upload.component.css']
})
export class DocumentUploadComponent {
  uploadForm: FormGroup;
  uploadSuccess: boolean = false;

  constructor(private fb: FormBuilder, private apiService: ApiService) {
    this.uploadForm = this.fb.group({
      title: ['', Validators.required],
      file: [null, Validators.required]
    });
  }

  onSubmit(): void {
    if (this.uploadForm.valid) {
      const formData = new FormData();
      formData.append('title', this.uploadForm.get('title')?.value);
      formData.append('file', this.uploadForm.get('file')?.value);
      formData.append('userId', '1'); // Replace '1' with the actual user ID (e.g., from authentication)
  
      this.apiService.uploadDocument(formData).subscribe({
        next: (response) => {
          console.log('Upload successful:', response);
          this.uploadSuccess = true;
          this.uploadForm.reset();
        },
        error: (error) => {
          console.error('Upload failed:', error);
          this.uploadSuccess = false;
        }
      });
    }
  }

  onFileChange(event: any): void {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.uploadForm.get('file')?.setValue(file);
    }
  }
}