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

  add(script: Script): void {
    script.name = "";
    script.body = "";
    script.author = "";
    script.categoryId = 0;
    script.version = 0;
    script.updateDataTime = 0;
    script.isLastVersion = true;
    if (!name) { return; }
    this.scriptService.addScript(script)
    .subscribe(script => this.scripts.push(script))
  }

}
