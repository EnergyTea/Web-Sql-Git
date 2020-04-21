import { Injectable } from '@angular/core';

import { Observable, of } from 'rxjs';
import { SCRIPTS } from './shared/mock-scripts';
import { Script } from './shared/Script';



@Injectable({ providedIn: 'root' })
export class ScriptService {
  constructor() { }

  getScripts(): Observable<Script[]> {
    return of(SCRIPTS);
  }

  getScript(id: number): Observable<Script> {
    return of(SCRIPTS.find(script => script.id === id))
  }
}
