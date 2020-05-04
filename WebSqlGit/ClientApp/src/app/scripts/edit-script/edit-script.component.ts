import { Component, OnInit, Input } from '@angular/core';
import { Script } from '../shared/Script';
import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { ScriptService } from '../shared/script.service';

@Component({
    selector: 'app-edit',
    templateUrl: './edit-script.component.html',
    styleUrls: ['./edit-script.component.css']
})
/** edit component*/
export class EditScriptComponent implements OnInit {
  /** edit ctor */
  script: Script;
  edit: boolean;

  constructor(
    private route: ActivatedRoute,
    private scriptScrvice: ScriptService,
    private location: Location,
  ) { }

  ngOnInit(): void {
    this.getScript();
  }

  getScript(): void {
    const ScriptId = + this.route.snapshot.paramMap.get('ScriptId');
    this.scriptScrvice.getScript(ScriptId)
      .subscribe(script => this.script = script);
  }

  goBack(): void {
    this.location.back();
  }

  save(): void {
    this.scriptScrvice.upDateScript(this.script)
      .subscribe(() => this.goBack());
  }
}
