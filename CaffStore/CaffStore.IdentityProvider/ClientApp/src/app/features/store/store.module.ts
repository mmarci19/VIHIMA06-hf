import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UploadComponent } from './pages/upload/upload.component';
import { BrowseComponent } from './pages/browse/browse.component';

@NgModule({
  declarations: [
    UploadComponent,
    BrowseComponent,
    UploadComponent,
    BrowseComponent,
  ],
  imports: [CommonModule],
})
export class StoreModule {}
