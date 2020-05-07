import { Component, OnInit } from '@angular/core';
import { Script } from '../shared/Script';
import { ScriptService } from '../shared/script.service';


@Component({
  selector: 'app-scripts',
  templateUrl: './script-list.component.html',
  styleUrls: ['./script-list.component.css']
})
/** scripts component*/
export class ScriptsComponent implements OnInit {
  scripts: Script[];
  selectScripts: Script;


  constructor(private scriptService: ScriptService) { }

  ngOnInit() {
    this.getScripts();
  }

  getScripts(): void {
    this.scriptService.getScripts()
      .subscribe(scripts => this.scripts = scripts)
  }

  delete(script: Script): void {
    this.scripts = this.scripts.filter(s => s !== script);
    this.scriptService.deleteScript(script.id);
  }

  onSelect(script: Script): void {
    this.selectScripts = script;
  }
}
