import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { StoreClient, UploadedImagesResponseDto } from 'src/app/shared';

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
}
