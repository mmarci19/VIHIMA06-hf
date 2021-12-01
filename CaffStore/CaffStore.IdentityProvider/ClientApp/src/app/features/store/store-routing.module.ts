import { BrowseComponent } from './pages/browse/browse.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UploadComponent } from './pages/upload/upload.component';

const routes: Routes = [
  { path: '', component: BrowseComponent },
  { path: 'browse', component: BrowseComponent },
  { path: 'upload', component: UploadComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class StoreRoutingModule {}
