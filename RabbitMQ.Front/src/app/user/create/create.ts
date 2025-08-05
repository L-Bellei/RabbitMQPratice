import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { UserService } from '../user.service';
import { MatSnackBar } from '@angular/material/snack-bar';

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
  role = '';
  error = '';
  loading = false;

  constructor(
    private userService: UserService,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  showSuccessMessage() {
    this.snackBar.open('Usuário adicionado a fila para criação!', 'Fechar', {
      duration: 3000,
      panelClass: ['success-snackbar'],
      horizontalPosition: 'right',
      verticalPosition: 'top',
    });
  }

  createUser() {
    const user = {
      name: this.name,
      email: this.email,
      password: this.password,
      role: this.role,
    };
    this.userService.createUser(user).subscribe({
      next: () => {
        this.showSuccessMessage();
        this.router.navigate(['/users']);
      },
      error: () => {
        this.error = 'Erro ao criar usuário.';
      },
    });
  }
}
