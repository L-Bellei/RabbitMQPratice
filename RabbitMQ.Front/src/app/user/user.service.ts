import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

export interface User {
  id: number;
  name: string;
  email: string;
  createdAt?: string;
  updatedAt?: string;
  role: string;
}

@Injectable({ providedIn: 'root' })
export class UserService {
  private baseUrl = 'https://localhost:7118/api/User';

  constructor(private http: HttpClient) {}

  getAllUsers() {
    return this.http.get<{ data: User[] }>(this.baseUrl);
  }

  getUserById(id: number) {
    return this.http.get<User>(`${this.baseUrl}/${id}`);
  }

  createUser(user: Partial<User>) {
    return this.http.post(this.baseUrl, user);
  }

  updateUser(id: number, user: Partial<User>) {
    return this.http.put(`${this.baseUrl}/${id}`, user);
  }

  deleteUser(id: number) {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }
}
