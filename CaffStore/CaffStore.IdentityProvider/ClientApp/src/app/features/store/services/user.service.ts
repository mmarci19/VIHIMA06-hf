import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UploadedImagesResponseDto } from 'src/app/shared';
import { UserClient, UserDto } from 'src/app/shared/users.clients';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private client: UserClient) {}

  getUsers(): Observable<UserDto[]> {
    return this.client.get();
  }

  setUsername(userId: string, userName: string): Observable<void> {
    return this.client.setUsername(userId, userName);
  }
}
