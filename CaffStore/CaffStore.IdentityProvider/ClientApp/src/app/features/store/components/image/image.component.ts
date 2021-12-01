import { Component, Inject, Input } from '@angular/core';
import { API_BASE_URL, UploadedImagesResponseDto } from 'src/app/shared';

@Component({
  selector: 'app-image',
  templateUrl: './image.component.html',
  styleUrls: ['./image.component.css'],
})
export class ImageComponent {
  @Input() public image: UploadedImagesResponseDto =
    new UploadedImagesResponseDto();
  baseUrl: string = '';

  constructor(@Inject(API_BASE_URL) baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  getResourceURL(path: string | undefined): string {
    if (!path) return '';

    return this.baseUrl + '/' + path;
  }

  download(path: string | undefined, fileName: string | undefined): void {
    const link = document.createElement('a');
    link.setAttribute('target', '_blank');
    link.setAttribute('href', this.getResourceURL(path));
    link.setAttribute('download', fileName ?? 'image.caff');
    document.body.appendChild(link);
    link.click();
    link.remove();
  }
}
