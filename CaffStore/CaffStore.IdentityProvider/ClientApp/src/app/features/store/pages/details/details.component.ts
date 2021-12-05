import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { API_BASE_URL, CommentDto, DetailsDto } from 'src/app/shared';
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
  isAuthorized = false;

  constructor(
    @Inject(API_BASE_URL) baseUrl: string,
    private route: ActivatedRoute,
    private service: StoreService,
    private auth: AuthorizeService
  ) {
    this.baseUrl = baseUrl;
  }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id') ?? '';
    this.auth.isAuthenticated().subscribe((resp) => (this.isAuthorized = resp));
    this.loadImage();
  }

  getResourceURL(path: string | undefined): string {
    if (!path) return '';

    return this.baseUrl + '/' + path;
  }

  loadImage(): void {
    this.service.getImageById(this.id).subscribe((resp) => {
      this.image = resp;
      this.image.comments = this.image.comments?.reverse();
    });
  }

  sendNewComment(): void {
    this.service
      .addComment(this.id, new CommentDto({ content: this.newCommentText }))
      .subscribe(() => this.loadImage());
  }
}
