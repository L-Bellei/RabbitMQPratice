import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { UserService } from '../user.service';

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './list.html',
  styleUrls: ['./list.css'],
})
export class ListComponent implements OnInit {
  users: any[] = [];
  loading = false;
  error = '';

  constructor(private userService: UserService, private router: Router) {}

  ngOnInit() {
    this.loading = true;
    this.userService.getAllUsers().subscribe({
      next: (res) => {
        this.users = res.data;
        this.loading = false;
      },
      error: () => {
        this.error = 'Erro ao carregar usuários.';
        this.loading = false;
      },
    });
  }

  goToEdit(id: number) {
    this.router.navigate(['/users/edit', id]);
  }

  goToCreate() {
    this.router.navigate(['/users/create']);
  }

  goToDetails(id: number) {
    this.router.navigate(['/users/details', id]);
  }
}
