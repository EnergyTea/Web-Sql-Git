import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Script } from './Script';
import { SCRIPTS } from './SCRIPTS';

@Injectable({ providedIn: 'root' })
export class ScriptService {
  private scriptsUrl = 'api/scripts';  // URL to web api

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  getScripts(): Observable<Script[]> {
    return this.http.get<Script[]>(this.scriptsUrl);
  }

  getUserScripts(): Observable<Script[]> {
    const url = `${this.scriptsUrl}/user`;
    return this.http.get<Script[]>(url);
  }

  upDateScript(script: Script): Observable<Script> {
    const url = `${this.scriptsUrl}/${script.scriptId}/edit`;
    return this.http.post<Script>(url, script, this.httpOptions);
  }

  getScript(ScriptId: number): Observable<Script> {
    const url = `${this.scriptsUrl}/${ScriptId}`;
    return this.http.get<Script>(url, { withCredentials: true });
  }

  addScript(script: Script): Observable<Script> {
    return this.http.post<Script>(this.scriptsUrl, script, this.httpOptions);
  }
  
  deleteScript(script: Script | number): Observable<Script> {
    const id = typeof script === 'number' ? script : script.versionId;
    const url = `${this.scriptsUrl}/${id}/delete`;
    return this.http.post<Script>(url, this.httpOptions)
  }

  getStoriesScript(ScriptId: number): Observable<Script[]> {
    const url = `${this.scriptsUrl}/${ScriptId}/all`;
    return this.http.get<Script[]>(url);
  }

  getStoryScript(ScriptId: number): Observable<Script> {
    const url = `${this.scriptsUrl}/${ScriptId}/history`;
    return this.http.get<Script>(url);
  }

  searchScript(term: string): Observable<Script[]> {
    if (!term.trim()) {
      return of([]);
    }
    /*return this.http.get<Script[]>(`${this.scriptsUrl}/&name=${term}`);*/
    return of(SCRIPTS)
  }

  getScriptsByName(): Observable<Script[]> {
    return;
  }
}
