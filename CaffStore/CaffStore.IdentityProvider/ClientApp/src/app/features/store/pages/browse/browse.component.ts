import { Component, OnInit } from '@angular/core';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UploadedImagesResponseDto } from 'src/app/shared';
import { StoreService } from '../../services/store.service';

@Component({
  selector: 'app-browse',
  templateUrl: './browse.component.html',
  styleUrls: ['./browse.component.css'],
})
export class BrowseComponent implements OnInit {
  images: UploadedImagesResponseDto[] = [];
  searchText?: string;
  isLoading = true;
  isAuthenticated = false;

  constructor(
    private service: StoreService,
    private authService: AuthorizeService
  ) {}

  ngOnInit(): void {
    this.loadImages();
    this.authService
      .isAuthenticated()
      .subscribe((resp) => (this.isAuthenticated = resp));
  }

  loadImages(): void {
    this.isLoading = true;
    this.service.getUploadedImages(this.searchText).subscribe((resp) => {
      this.images = resp;
      this.isLoading = false;
    });
  }
}
