import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

interface User {
  id: string;
  name: string;
  email: string;
  role: string;
}

interface AuthResult {
  user: User;
  token: string;
}

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  private apiUrl = 'https://localhost:7118/api/login/authenticate';

  constructor(private http: HttpClient) {}

  login(identifier: string, password: string): Observable<AuthResult> {
    return this.http.post<AuthResult>(this.apiUrl, { identifier, password });
  }
}
