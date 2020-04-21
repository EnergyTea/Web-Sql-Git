import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { Script } from '../shared/Script';
import { ScriptService } from '../script.service'

@Component({
    selector: 'app-script',
    templateUrl: './script.component.html',
     styleUrls: ['./script.component.css', './light.min.css']
})
/** script component*/
export class ScriptComponent implements OnInit {
/** script ctor */
  script: Script;

  constructor(
    private route: ActivatedRoute,
    private scriptScrvice: ScriptService,
    private location: Location
  ) { }

  ngOnInit(): void {
    this.getScript();
  }

  getScript(): void {
    const id = + this.route.snapshot.paramMap.get('id');
    this.scriptScrvice.getScript(id)
      .subscribe(script => this.script = script);
  }


}
