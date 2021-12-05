import {
  Component,
  EventEmitter,
  Inject,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { API_BASE_URL, UploadedImagesResponseDto } from 'src/app/shared';
import { StoreService } from '../../services/store.service';

@Component({
  selector: 'app-image',
  templateUrl: './image.component.html',
  styleUrls: ['./image.component.css'],
})
export class ImageComponent implements OnInit {
  @Input() public image: UploadedImagesResponseDto =
    new UploadedImagesResponseDto();
  baseUrl: string = '';

  @Output() public onDelete = new EventEmitter();
  role: string = '';

  constructor(
    @Inject(API_BASE_URL) baseUrl: string,
    private service: StoreService,
    private authService: AuthorizeService
  ) {
    this.baseUrl = baseUrl;
  }
  ngOnInit(): void {
    this.authService.getRole().subscribe((resp) => (this.role = resp));
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

  delete(): void {
    if (this.image.id) {
      this.service
        .deleteImage(this.image.id)
        .subscribe(() => this.onDelete.emit());
    }
  }
}
