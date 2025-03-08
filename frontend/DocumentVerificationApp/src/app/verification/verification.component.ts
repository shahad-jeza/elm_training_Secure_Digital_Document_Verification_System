import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ApiService } from '../services/api.service';
import { CommonModule, DatePipe } from '@angular/common'; // Add DatePipe

@Component({
  selector: 'app-verification',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, DatePipe], // Add DatePipe
  templateUrl: './verification.component.html',
  styleUrls: ['./verification.component.css']
})
export class VerificationComponent {
  verificationForm: FormGroup;
  verificationResult: any = null;

  constructor(private fb: FormBuilder, private apiService: ApiService) {
    this.verificationForm = this.fb.group({
      verificationCode: ['', Validators.required]
    });
  }

  onSubmit(): void {
    if (this.verificationForm.valid) {
      const verificationData = {
        documentId: this.verificationForm.get('documentId')?.value,
        verifiedByUserId: this.verificationForm.get('verifiedByUserId')?.value,
        timestamp: new Date().toISOString(), // Current timestamp
        status: 'Verified' // Set the status explicitly
      };
  
      this.apiService.verifyDocument(verificationData).subscribe({
        next: (response) => {
          console.log('Verification successful:', response);
          this.verificationResult = response;
        },
        error: (error) => {
          console.error('Verification failed:', error);
          this.verificationResult = null;
        }
      });
    }
  }
}