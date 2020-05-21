import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Category } from './Category';
import { Script } from '../../scripts/shared/Script';

@Injectable({providedIn: 'root'})
export class CategoryService {

  private categotyUrl = 'https://localhost:44325/api/categories';

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

  deleteCategory(category: Category | number): Observable<Category> {
    const id = typeof category === 'number' ? category : category.id;
    const url = `${this.categotyUrl}/${id}/delete`;
    return this.http.post<Category>(url, this.httpOptions)
  }

  getScripts(CategoryId: number): Observable<Script[]> {
    const url = `${this.categotyUrl}/${CategoryId}/scripts`;
    return this.http.get<Script[]>(url)
  }

  addCategory(category: Category): Observable<Category> {
    return this.http.post<Category>(this.categotyUrl, category, this.httpOptions)
  }
}
