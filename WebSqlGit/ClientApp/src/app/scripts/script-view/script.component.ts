import { Location } from '@angular/common';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../../authentication/shared/user.service';
import { Script } from '../shared/Script';
import { ScriptService } from '../shared/script.service';

@Component({
    selector: 'app-script',
    templateUrl: './script.component.html',
     styleUrls: ['./script.component.css']
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
    this.toggleHighlight(0);
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
        if (!script.isLastVersion) {
          this.script.isAuthor = false;
        }
        this.highlight();
      });
    this.userService.getUser().subscribe(user => {
      if (user.name == null) {
        this.isAurorize = false;
      }
    })
  }

  highlight(): void {
    setTimeout(function () {      
      document.querySelectorAll('pre code').forEach((block) => {
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

  deleteScript(script: Script): void {
    this.scriptScrvice.deleteScript(script.scriptId)
      .subscribe(data => { this.getScript(); this.goBack() });
  }
}
