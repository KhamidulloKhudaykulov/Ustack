import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule, HttpClientModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  error: string = '';
  message: string = '';

  constructor(private router: Router, private http: HttpClient) { }

  navigateToResetPassword() {
    this.router.navigate(['/reset-password']);
  }

  onConfirm() {
    const url = 'https://localhost:7293/api/identity/users/login';
    this.http.post(url, { username: this.email, password: this.password }).subscribe({
      next: () => {
        this.message = 'Muvaffaqiyatli!';
        this.error = '';
      },
      error: (err) => {
        this.error = 'Xatolik yuz berdi: ' + (err.error?.message || err.statusText);
        this.message = '';
      }
    });
  }
}
