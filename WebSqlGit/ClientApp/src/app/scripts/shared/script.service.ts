import { Injectable } from '@angular/core';

import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Script } from './Script';

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

  upDateScript(script: Script): Observable<Script> {
    const url = `${this.scriptsUrl}/${script.scriptId}/edit`;
    return this.http.post<Script>(url, script, this.httpOptions);
  }

  getScript(ScriptId: number): Observable<Script> {
    const url = `${this.scriptsUrl}/${ScriptId}`;
    return this.http.get<Script>(url);
  }

  addScript(script: Script): Observable<Script> {
    return this.http.post<Script>(this.scriptsUrl, script, this.httpOptions);
  }
  
  deleteScript(script: Script | number): Observable<Script> {
    const id = typeof script === 'number' ? script : script.id;
    const url = `${this.scriptsUrl}/${id}/delete`;
    return this.http.post<Script>(url, this.httpOptions)
  }

  deleteVersion(script: Script | number): Observable<Script> {
    const id = typeof script === 'number' ? script : script.id;
    console.log(script);
    const url = `${this.scriptsUrl}/version/${id}/delete`;
    return this.http.post<Script>(url, this.httpOptions)
  }

  getVersionScript(ScriptId: number): Observable<Script[]> {
    const url = `${this.scriptsUrl}/${ScriptId}/all`;
    return this.http.get<Script[]>(url);
  }
}
