import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UploadComponent } from './pages/upload/upload.component';
import { BrowseComponent } from './pages/browse/browse.component';
import { ImageComponent } from './components/image/image.component';
import { BrowserModule } from '@angular/platform-browser';
import { StoreRoutingModule } from './store-routing.module';

@NgModule({
  declarations: [
    UploadComponent,
    BrowseComponent,
    UploadComponent,
    BrowseComponent,
    ImageComponent,
  ],
  imports: [StoreRoutingModule, CommonModule, BrowserModule],
  exports: [ImageComponent],
})
export class StoreModule {}
