import { Component, OnInit } from '@angular/core';
import { ScriptService } from '../script.service';
import { Script } from '../shared/Script';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
/** create component*/
export class CreateComponent implements OnInit {
  /** create ctor */
  scripts: Script[];

  constructor(private scriptService: ScriptService) { }

  ngOnInit() { }

  add(name: string): void {
    name = name.trim();
    if (!name) { return; }
    this.scriptService.addScript({ name } as Script)
    .subscribe(script => this.scripts.push(script))
  }

}
