import { Component, OnInit } from '@angular/core';
import { Script } from '../Script';
import { SCRIPTS } from '../mock-scripts';

@Component({
    selector: 'app-script-list',
    templateUrl: './script-list.component.html',
    styleUrls: ['./script-list.component.css']
})
/** script-list component*/
export class ScriptListComponent implements OnInit {

  scripts = SCRIPTS;
  selectScripts: Script

  ngOnInit() { }

  onSelect(script: Script): void {
    this.selectScripts = script;
  }
}
