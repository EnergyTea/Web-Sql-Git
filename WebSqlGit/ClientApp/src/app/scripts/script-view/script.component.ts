import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { Script } from '../shared/Script';
import { ScriptService } from '../shared/script.service';

@Component({
    selector: 'app-script',
    templateUrl: './script.component.html',
     styleUrls: ['./script.component.css', './light.min.css']
})
/** script component*/
export class ScriptComponent implements OnInit {
/** script ctor */
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