import { Component, OnInit } from '@angular/core';
import { UploadedImagesResponseDto } from 'src/app/shared';
import { StoreService } from '../../services/store.service';
import { ImageComponent } from '../../components/image/image.component';

@Component({
  selector: 'app-browse',
  templateUrl: './browse.component.html',
  styleUrls: ['./browse.component.css'],
})
export class BrowseComponent implements OnInit {
  images: UploadedImagesResponseDto[] = [];
  constructor(private service: StoreService) {}

  ngOnInit(): void {
    this.service.getUploadedImages().subscribe((resp) => (this.images = resp));
  }
}
