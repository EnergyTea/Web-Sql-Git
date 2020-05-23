import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { Script } from '../shared/Script';
import { ScriptService } from '../shared/script.service';
import { using, timer } from 'rxjs';
import { UserService } from '../../authentication/shared/user.service';

@Component({
    selector: 'app-script',
    templateUrl: './script.component.html',
     styleUrls: ['./script.component.css', './light.min.css']
})
/** script component*/
export class ScriptComponent implements OnInit {
/** script ctor */
  script = <Script>{};
  scripts: Script[];
  edit: boolean;
  isAurorize = true;
  public highlightedDiv: number;

  constructor(
    private route: ActivatedRoute,
    private scriptScrvice: ScriptService,
    private location: Location,
    private userService: UserService
  ) { }


  public toggleHighlight(newValue: number) {
    this.highlightedDiv = newValue;
  }

  ngOnInit(): void {
    this.getScript();
    this.getAll();
  }

  getScript(): void {
    const ScriptId = + this.route.snapshot.paramMap.get('ScriptId');
    this.scriptScrvice.getScript(ScriptId)
      .subscribe(script => {
        this.script = script;
        setTimeout(function () {
          document.querySelectorAll('pre code').forEach((block) => {
            hljs.highlightBlock(block);
        }, 2000);        
      }); })
    this.userService.getUser().subscribe(user => {
      if (user.name == null) {
        this.isAurorize = false
      }
    })
  }

  getScriptHistory(id: number): void {
    this.scriptScrvice.getVerScrOne(id)
      .subscribe(script => {
        this.script = script;
        setTimeout(function () {
        document.querySelectorAll('pre code').forEach((block) => {
          hljs.highlightBlock(block);
        }, 2000);
      }); });
    this.userService.getUser().subscribe(user => {
      if (user.name == null) {
        this.isAurorize = false;
      }
    })
  }

  goBack(): void {
    this.location.back();
  }

  getAll(): void {
    const ScriptId = + this.route.snapshot.paramMap.get('ScriptId');
    this.scriptScrvice.getVersionScript(ScriptId)
      .subscribe(scripts => this.scripts = scripts.reverse());
  }

  goTo(script: Script) {
    this.getScriptHistory(script.versionId);
  }

  delete(script: Script): void {
    this.scriptScrvice.deleteScript(script.scriptId)
      .subscribe(data => { this.getScript(); this.goBack() });
  }
}
