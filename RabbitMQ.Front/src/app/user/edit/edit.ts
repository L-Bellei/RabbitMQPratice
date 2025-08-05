import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { UserService } from '../user.service';

interface User {
  id: number;
  name: string;
  email: string;
  role: string;
}

@Component({
  selector: 'app-user-edit',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './edit.html',
  styleUrls: ['./edit.css'],
})
export class EditComponent implements OnInit {
  id!: number;
  name = '';
  email = '';
  error = '';
  role = '';
  loading = false;

  constructor(
    private userService: UserService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
    this.loading = true;
    this.userService.getUserById(this.id).subscribe({
      next: (user) => {
        this.name = user.name;
        this.email = user.email;
        this.role = user.role || '';
        this.loading = false;
      },
      error: () => {
        this.error = 'Erro ao carregar usuário.';
        this.loading = false;
      },
    });
  }

  updateUser() {
    this.loading = true;
    const user: User = {
      id: this.id,
      name: this.name,
      email: this.email,
      role: this.role,
    };
    this.userService.updateUser(this.id, user).subscribe({
      next: () => {
        this.loading = false;
        this.router.navigate(['/users']);
      },
      error: () => {
        this.error = 'Erro ao atualizar usuário.';
        this.loading = false;
      },
    });
  }
}
