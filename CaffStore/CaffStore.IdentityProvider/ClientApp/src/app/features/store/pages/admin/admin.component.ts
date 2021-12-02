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
  displayedColumns: string[] = ['id', 'username', 'save'];

  currentlyEditedIndex = -1;

  constructor(private service: UserService) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers() {
    this.service.getUsers().subscribe((resp) => (this.users = resp));
  }

  setCurrentlyEditedIndex(index: number): void {
    this.currentlyEditedIndex = index;
  }

  saveEdit(id: string, userName: string) {
    console.log(id, userName);
    this.service.setUsername(id, userName).subscribe(() => {
      this.loadUsers();
      this.currentlyEditedIndex = -1;
    });
  }
}
