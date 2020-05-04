import { Component, OnInit } from '@angular/core';
import { Script } from '../shared/Script';
import { ScriptService } from '../shared/script.service';

@Component({
  selector: 'app-create',
  templateUrl: './create-script.component.html',
  styleUrls: ['./create-script.component.css']
})
/** create component*/
export class CreateScriptComponent implements OnInit {
  /** create ctor */
  scripts: Script[];

  constructor(private scriptService: ScriptService) { }

  ngOnInit() { }

  add(name: string): void {
    name = name.trim();
    if (!name) { return; }
    this.scriptService.addScript({ name } as Script)
    .subscribe(script => this.scripts.push(script))
  }

}
