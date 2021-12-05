import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {
  CommentDto,
  DetailsDto,
  StoreClient,
  UploadedImagesResponseDto,
} from 'src/app/shared';

@Injectable({
  providedIn: 'root',
})
export class StoreService {
  constructor(private client: StoreClient) {}

  getUploadedImages(
    searchText: string | null | undefined
  ): Observable<UploadedImagesResponseDto[]> {
    return this.client.browseImages(searchText);
  }

  getImageById(id: string): Observable<DetailsDto> {
    return this.client.getImageById(id);
  }

  addComment(id: string, dto: CommentDto): Observable<void> {
    return this.client.addComment(id, dto);
  }

  deleteImage(id: string): Observable<void> {
    return this.client.deleteImage(id);
  }
}
