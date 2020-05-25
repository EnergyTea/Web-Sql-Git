import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Script } from '../../scripts/shared/Script';
import { Category } from './Category';

@Injectable({providedIn: 'root'})
export class CategoryService {

  private categotyUrl = 'api/categories';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  getCategory(CategoryId: number): Observable<Category> {
    const url = `${this.categotyUrl}/${CategoryId}`;
    return this.http.get<Category>(url);

  }

  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(this.categotyUrl)
  }

  getScripts(CategoryId: number): Observable<Script[]> {
    const url = `${this.categotyUrl}/${CategoryId}/scripts`;
    return this.http.get<Script[]>(url)
  }

  addCategory(category: Category): Observable<Category> {
    return this.http.post<Category>(this.categotyUrl, category, this.httpOptions)
  }
}
