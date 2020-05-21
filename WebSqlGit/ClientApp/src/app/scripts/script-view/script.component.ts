import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { Script } from '../shared/Script';
import { ScriptService } from '../shared/script.service';
import { using } from 'rxjs';
import { UserService } from '../../authentication/shared/user.service';

@Component({
    selector: 'app-script',
    templateUrl: './script.component.html',
     styleUrls: ['./script.component.css', './light.min.css']
})
/** script component*/
export class ScriptComponent implements OnInit {
/** script ctor */
  script: Script;
  scripts: Script[];
  edit: boolean;
  isAurorize = true;
  isActive = false;

  constructor(
    private route: ActivatedRoute,
    private scriptScrvice: ScriptService,
    private location: Location,
    private userService: UserService
  ) { }

  ngOnInit(): void {
    this.getScript();
    this.getAll();
  }

  getScript(): void {
    const ScriptId = + this.route.snapshot.paramMap.get('ScriptId');
    this.scriptScrvice.getScript(ScriptId)
      .subscribe(script => this.script = script)
    this.userService.getUser().subscribe(user => {
      if (user.name == null) {
        this.isAurorize = false
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

  getScriptHistory(id: number): void {
    this.scriptScrvice.getVerScrOne(id)
      .subscribe(script => this.script = script);
    this.userService.getUser().subscribe(user => {
      if (user.name !== null) {
        this.isAurorize = true
      }
    })
  }

  goTo(script: Script) {
    this.location.go('/scripts/' + script.id)
    this.getScriptHistory(script.id);
  }

  delete(script: Script): void {
    this.scriptScrvice.deleteVersion(script.id)
      .subscribe(data => { this.getScript(); this.goBack() });
  }
}
