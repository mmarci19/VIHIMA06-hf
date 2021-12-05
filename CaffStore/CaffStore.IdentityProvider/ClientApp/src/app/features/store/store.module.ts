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
import { AdminComponent } from './pages/admin/admin.component';
import { MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { DetailsComponent } from './pages/details/details.component';
import { CommentComponent } from './components/comment/comment.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@NgModule({
  declarations: [
    UploadComponent,
    BrowseComponent,
    UploadComponent,
    BrowseComponent,
    ImageComponent,
    AdminComponent,
    DetailsComponent,
    CommentComponent,
  ],
  imports: [
    StoreRoutingModule,
    CommonModule,
    BrowserModule,
    MatCardModule,
    MatButtonModule,
    MatDividerModule,
    FormsModule,
    MatTableModule,
    MatFormFieldModule,
    MatInputModule,
    MatProgressSpinnerModule,
  ],
  exports: [ImageComponent],
})
export class StoreModule {}
