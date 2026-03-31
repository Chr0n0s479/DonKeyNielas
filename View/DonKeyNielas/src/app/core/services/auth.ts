import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RegisterRequest } from '../models/Iregister-request.model';
import { AuthResponse } from '../models/Iauth-response.model';
import { LoginRequest } from '../models/Ilogin-request.model';

@Injectable({
  providedIn: 'root',
})
export class Auth {
  private API = "http://localhost:5262/api/auth";

  constructor(private http: HttpClient) { }


  register(data: RegisterRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(
      `${this.API}/register`,
      data
    )
  }

  login(data: LoginRequest): Observable<AuthResponse> {

    return this.http.post<AuthResponse>(
      `${this.API}/login`,
      data
    )
  }

  getRole(): string | null {

  const token = localStorage.getItem('token');

  if (!token) return null;

  const payload = JSON.parse(atob(token.split('.')[1]));

  return payload["role"] || payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

}
isAdmin(): boolean {
  return this.getRole() === 'Admin';
}

}
