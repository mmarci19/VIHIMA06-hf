import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FileResponse, StoreClient } from 'src/app/shared';

@Injectable({
  providedIn: 'root',
})
export class StoreService {
  constructor(private client: StoreClient) {}
}
