import { Component, OnInit } from '@angular/core';
import { AuthorizeService } from 'src/api-authorization/authorize.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css'],
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  role: string = '';
  isAuthenticated: boolean = false;

  constructor(private authService: AuthorizeService) {}

  ngOnInit(): void {
    this.authService.isAuthenticated().subscribe((resp) => {
      this.isAuthenticated = resp;
      this.authService.getRole().subscribe((resp) => {
        this.role = resp;
      });
    });
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  isAdmin = (): boolean => this.isAuthenticated && this.role === 'Admin';
}
