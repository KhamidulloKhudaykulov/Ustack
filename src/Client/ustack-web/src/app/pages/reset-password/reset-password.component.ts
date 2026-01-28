import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-reset-password',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule, HttpClientModule],
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent {
  email: string = '';
  secretCode: string = '';
  newPassword: string = '';
  message: string = '';
  error: string = '';
  showSecretInput: boolean = false;
  isTokenConfirmed: boolean = false;

  constructor(private router: Router, private http: HttpClient) {}

  navigateToLogin() {
    this.router.navigate(['/login']);
  }

  // 1️⃣ Maxfiy kodni olish
  sendResetCode() {
    if (!this.email) {
      this.showError('Email kiriting');
      return;
    }

    const url = 'https://localhost:7293/api/identity/users/reset-password';
    this.http.post(url, { email: this.email }).subscribe({
      next: () => {
        this.showMessage('Maxfiy kod yuborildi. Emailingizni tekshiring.');
        this.showSecretInput = true; // inputni ko‘rsatish
      },
      error: (err) => this.showError('Xatolik yuz berdi: ' + (err.error?.message || err.statusText))
    });
  }

  // 2️⃣ Maxfiy kodni tasdiqlash
  confirmSecretCode() {
    if (!this.secretCode) {
      this.showError('Maxfiy kod kiriting');
      return;
    }

    const url = 'https://localhost:7293/api/identity/users/confirm-reset-password-token';
    this.http.post(url, { email: this.email, token: this.secretCode }).subscribe({
      next: () => {
        this.showMessage('Maxfiy kod tasdiqlandi!');
        this.showSecretInput = false;
        this.isTokenConfirmed = true;},
      error: (err) => this.showError('Xatolik yuz berdi: ' + (err.error?.message || err.statusText))
    });
  }

  confirmNewPassword() {
    if (!this.newPassword) {
      this.showError('Parolni kiriting!');
      return;
    }
    const url = 'https://localhost:7293/api/identity/users/confirm-reset-password';
    this.http.post(url, { email: this.email, newPassword: this.newPassword }).subscribe({
      next: () => {
        this.showMessage('Yangi parol o‘rnatildi!')
        setTimeout(() => this.navigateToLogin(), 2000);
      }
    })
  }

  // 3️⃣ Helper functions
  showMessage(msg: string) {
    this.message = msg;
    this.error = '';
    setTimeout(() => this.message = '', 4000);
  }

  showError(msg: string) {
    this.error = msg;
    this.message = '';
    setTimeout(() => this.error = '', 4000);
  }
}
