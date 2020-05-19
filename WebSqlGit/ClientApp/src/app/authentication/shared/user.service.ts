import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { User } from './User';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class UserService {

  private authorizeUrl = 'api/account';
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }


  authorizationUser(user: User): Observable<User> {
    return this.http.post<User>(this.authorizeUrl, user, this.httpOptions);
  }
}
