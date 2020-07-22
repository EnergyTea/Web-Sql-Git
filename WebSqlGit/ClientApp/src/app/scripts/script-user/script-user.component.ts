import { Component } from '@angular/core';
import { Script } from '../shared/Script';
import { ScriptService } from '../shared/script.service';

@Component({
    selector: 'app-home',
    templateUrl: './script-user.component.html',
    styleUrls: ['./script-user.component.css']
})

/** script-user component*/
export class ScriptUserComponent {
    /** script-user ctor */
  scripts: Script[];

  constructor(private scriptService: ScriptService) { }

  ngOnInit() {
    this.getScripts();
  }

  getScripts(): void {
    this.scriptService.getUserScripts()
      .subscribe(scripts => this.scripts = scripts.reverse())
  }
}
