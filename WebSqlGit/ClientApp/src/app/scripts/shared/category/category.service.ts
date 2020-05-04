import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Category } from './Category';

@Injectable({providedIn: 'root'})
export class CategoryService {

  private categotyUrl = 'api/categories';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  getCategory(id: number): Observable<Category> {
    const url = `${this.categotyUrl}/${id}`;
    return this.http.get<Category>(url);

  }

  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(this.categotyUrl)
  }

  deleteCategory(category: Category | number): Observable<Category> {
    const id = typeof category === 'number' ? category : category.id;
    const url = `${this.categotyUrl}/${id}`;

    return this.http.delete<Category>(url, this.httpOptions)
  }

  addCategory(category: Category): Observable<Category> {
    return this.http.post<Category>(this.categotyUrl, category, this.httpOptions)
  }
}
