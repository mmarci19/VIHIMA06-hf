import { Component, OnInit } from '@angular/core';
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

  constructor(private service: StoreService) {}

  ngOnInit(): void {
    this.loadImages();
  }

  loadImages(): void {
    this.isLoading = true;
    this.service.getUploadedImages(this.searchText).subscribe((resp) => {
      this.images = resp;
      this.isLoading = false;
    });
  }
}
