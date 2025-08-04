import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { UserService } from '../user.service';

@Component({
  selector: 'app-user-create',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './create.html',
  styleUrls: ['./create.css'],
})
export class CreateComponent {
  name = '';
  email = '';
  password = '';
  error = '';

  constructor(private userService: UserService, private router: Router) {}

  createUser() {
    const user = {
      name: this.name,
      email: this.email,
      password: this.password,
    };
    this.userService.createUser(user).subscribe({
      next: () => {
        this.router.navigate(['/users']);
      },
      error: () => {
        this.error = 'Erro ao criar usuário.';
      },
    });
  }
}
