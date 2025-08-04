import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../login.service';
import { AuthService } from '../auth.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.html',
  styleUrls: ['./login.css'],
})
export class LoginComponent implements OnInit {
  identifier = '';
  password = '';
  error = '';

  constructor(
    private loginService: LoginService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit() {
    if (this.authService.isLoggedIn) {
      this.router.navigate(['/home']);
    }
  }

  login() {
    this.loginService.login(this.identifier, this.password).subscribe({
      next: (res) => {
        this.authService.login(res.token, res.user);
        this.router.navigate(['/home']);
      },
      error: () => {
        this.error = 'Usuário ou senha inválidos.';
      },
    });
  }
}
