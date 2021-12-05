import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BrowseComponent } from './features/store/pages/browse/browse.component';
import { UploadComponent } from './features/store/pages/upload/upload.component';

const routes: Routes = [
  { path: '', component: BrowseComponent },
  { path: 'browse', component: BrowseComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
