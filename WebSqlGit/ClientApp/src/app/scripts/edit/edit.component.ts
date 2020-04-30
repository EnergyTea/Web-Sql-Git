import { Component, OnInit, Input } from '@angular/core';
import { ScriptService } from '../script.service';
import { Script } from '../shared/Script';
import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-edit',
    templateUrl: './edit.component.html',
    styleUrls: ['./edit.component.css']
})
/** edit component*/
export class EditComponent implements OnInit {
/** edit ctor */
  script: Script;
  constructor(
    private scripitService: ScriptService,
    private location: Location,
    private route: ActivatedRoute,
    private scriptScrvice: ScriptService,
  ) {

      
  }
  goBack(): void {
    this.location.back();
  }

  save(): void {
    this.scripitService.upDateScript(this.script)
      .subscribe(() => this.goBack());
  }

  ngOnInit() {
    this.getScript();
  }

  getScript(): void {
    const id = + this.route.snapshot.paramMap.get('id');
    this.scriptScrvice.getScript(id)
      .subscribe(script => this.script = script);
  }
}
