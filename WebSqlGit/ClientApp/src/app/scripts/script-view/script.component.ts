import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
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
    private ref: ChangeDetectorRef,
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
        this.highlight();
      })
    this.userService.getUser().subscribe(user => {
      if (user.name == null) {
        this.isAurorize = false
      }
    })
  }
  getScriptHistory(id: number): void {
    this.script.body = null;
    this.ref.detectChanges();
    this.scriptScrvice.getStoryScript(id)
      .subscribe(script => {
        this.script = script;
        this.highlight();
      });
    this.userService.getUser().subscribe(user => {
      if (user.name == null) {
        this.isAurorize = false;
      }
    })
  }
    delay(ms: number) {
      return new Promise(resolve => setTimeout(resolve, ms));
    }

  highlight(): void {
    setTimeout(function () {
      
      document.querySelectorAll('pre code').forEach((block) => {
         // hljs.highlightBlock(block);
        window["hljs"].initHighlighting.called = false;
        window["hljs"].initHighlighting();
      }, 2000);
    });
  }

  goBack(): void {
    this.location.back();
  }

  getAll(): void {
    const ScriptId = + this.route.snapshot.paramMap.get('ScriptId');
    this.scriptScrvice.getStoriesScript(ScriptId)
      .subscribe(scripts => this.scripts = scripts.reverse());
  }

  delete(script: Script): void {
    this.scriptScrvice.deleteScript(script.scriptId)
      .subscribe(data => { this.getScript(); this.goBack() });
  }
}
