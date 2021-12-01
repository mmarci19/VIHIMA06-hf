import { Component, Inject, Input } from '@angular/core';
import { API_BASE_URL } from 'src/app/shared';

@Component({
  selector: 'app-image',
  templateUrl: './image.component.html',
  styleUrls: ['./image.component.css'],
})
export class ImageComponent {
  @Input() public source: string = '';
  baseUrl: string = '';

  constructor(@Inject(API_BASE_URL) baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  getGIfURL(gifPath: string): string {
    console.log(gifPath);
    console.log(this.baseUrl);
    return this.baseUrl + '/' + gifPath;
  }
}
