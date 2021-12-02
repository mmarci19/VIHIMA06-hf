import { Component, OnInit } from '@angular/core';
import { UserDto } from 'src/app/shared/users.clients';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css'],
})
export class AdminComponent implements OnInit {
  users: UserDto[] = [];
  constructor(private service: UserService) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers() {
    this.service.getUsers().subscribe((resp) => (this.users = resp));
  }
}
