import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UploadComponent } from './pages/upload/upload.component';
import { BrowseComponent } from './pages/browse/browse.component';
import { ImageComponent } from './components/image/image.component';
import { BrowserModule } from '@angular/platform-browser';
import { StoreRoutingModule } from './store-routing.module';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { FormsModule } from '@angular/forms';

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
    FormsModule,
  ],
  exports: [ImageComponent],
})
export class StoreModule {}
