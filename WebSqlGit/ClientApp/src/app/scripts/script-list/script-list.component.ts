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

  constructor(private scriptService: ScriptService) { }

  ngOnInit() {
    this.getScripts();
  }

  getScripts(): void {
    this.scriptService.getScripts()
      .subscribe(scripts => this.scripts = scripts.reverse())
  }
}
