import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UploadComponent } from './pages/upload/upload.component';
import { BrowseComponent } from './pages/browse/browse.component';
import { ImageComponent } from './components/image/image.component';
import { BrowserModule } from '@angular/platform-browser';
import { StoreRoutingModule } from './store-routing.module';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatDivider, MatDividerModule } from '@angular/material/divider';

@NgModule({
  declarations: [
    UploadComponent,
    BrowseComponent,
    UploadComponent,
    BrowseComponent,
    ImageComponent,
  ],
  imports: [
    StoreRoutingModule,
    CommonModule,
    BrowserModule,
    MatCardModule,
    MatButtonModule,
    MatDividerModule,
  ],
  exports: [ImageComponent],
})
export class StoreModule {}
