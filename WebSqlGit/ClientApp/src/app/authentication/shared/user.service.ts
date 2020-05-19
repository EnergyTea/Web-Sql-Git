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

  createUser(user: User): Observable<User> {
    const url = `${this.authorizeUrl}/regiser`;
    return this.http.post<User>(url, user, this.httpOptions);
  }

  authorizationUser(user: User): Observable<User> {
    return this.http.post<User>(this.authorizeUrl, user, this.httpOptions);
  }

  getUser(): Observable<User> {
    return this.http.get<User>(this.authorizeUrl)
  }

  outUser(): Observable<User> {
    const url = `${this.authorizeUrl}/logout`;
    return this.http.get<User>(url, { withCredentials: true })
  }
}
