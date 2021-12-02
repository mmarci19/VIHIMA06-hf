import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {
  API_BASE_URL,
  CommentDto,
  DetailsDto,
  UploadedImagesResponseDto,
} from 'src/app/shared';
import { StoreService } from '../../services/store.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css'],
})
export class DetailsComponent implements OnInit {
  image: DetailsDto = new DetailsDto();
  id: string = '';
  baseUrl = '';
  newCommentText: string = '';

  constructor(
    @Inject(API_BASE_URL) baseUrl: string,
    private route: ActivatedRoute,
    private service: StoreService
  ) {
    this.baseUrl = baseUrl;
  }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id') ?? '';
    this.loadImage();
  }

  getResourceURL(path: string | undefined): string {
    if (!path) return '';

    return this.baseUrl + '/' + path;
  }

  loadImage(): void {
    this.service.getImageById(this.id).subscribe((resp) => {
      this.image = resp;
      console.log(this.image);
    });
  }

  sendNewComment(): void {
    this.service
      .addComment(this.id, new CommentDto({ content: this.newCommentText }))
      .subscribe(() => this.loadImage());
  }
}
