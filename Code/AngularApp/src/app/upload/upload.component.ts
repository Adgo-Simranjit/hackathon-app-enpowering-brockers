import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-upload',
  standalone: true,
  imports: [CommonModule, HttpClientModule, FormsModule],
  templateUrl: './upload.component.html',
  styleUrl: './upload.component.css'
})
export class UploadComponent {
  
selectedFile: File | null = null;
responseData: any;
errorMessage: string | null = null;
description: string = '';
chatHistory: { userInput: string; response: string }[] = [];

constructor(private http: HttpClient) {}

onFileSelected(event: any) {
  this.selectedFile = event.target.files[0];
}

onSubmit(event: Event) {
  event.preventDefault();
  const formData = new FormData();
  formData.append('description', this.description);
  formData.append('threadId',"Empty");
  // if (this.selectedFile){
  //   formData.append('file', this.selectedFile);
  // }
  const url = 'http://localhost:5274/api/agent/upload';
  const url2 = 'http://localhost:5274/api/ExactApiAgent/pullData';

  this.http.post(url2, formData, { responseType: 'text' })
    .subscribe({
      next: (response) => {
        this.responseData = response;
        this.chatHistory.push({
          userInput: this.description,
          response: response
        });
      },
      error: (err) => {
        this.chatHistory.push({
          userInput: this.description,
          response: 'Error: ' + err.error
        });
      },
      complete: () => {
        console.log('Request completed.');
        this.description = '';
      }
    });
  }

}
