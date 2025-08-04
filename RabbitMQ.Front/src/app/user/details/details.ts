import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

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
    private router: Router
  ) {}

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
      },
      error: () => {
        this.error = 'Error loading user details.';
        this.loading = false;
      },
    });
  }
}
