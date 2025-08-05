import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-details',
  imports: [CommonModule, FormsModule],
  templateUrl: './details.html',
  styleUrl: './details.css',
})
export class DetailsComponent implements OnInit {
  id!: number;
  name = '';
  role = '';
  email = '';
  error = '';
  createdAt = '';
  updatedAt = '';
  loading = false;

  constructor(
    private userService: UserService,
    private route: ActivatedRoute,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  showSuccessMessage() {
    this.snackBar.open('Dados do usuário carregado!', 'Fechar', {
      duration: 3000,
      panelClass: ['success-snackbar'],
      horizontalPosition: 'right',
      verticalPosition: 'top',
    });
  }

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.userService.getUserById(id).subscribe({
      next: (user) => {
        this.id = user.id;
        this.name = user.name;
        this.email = user.email;
        this.role = user.role || '';
        this.createdAt = user.createdAt
          ? new Date(user.createdAt).toLocaleDateString()
          : '';
        this.updatedAt = user.updatedAt
          ? new Date(user.updatedAt).toLocaleDateString()
          : '';
        this.loading = false;

        this.showSuccessMessage();
      },
      error: () => {
        this.error = 'Error loading user details.';
        this.loading = false;
      },
    });
  }
}
