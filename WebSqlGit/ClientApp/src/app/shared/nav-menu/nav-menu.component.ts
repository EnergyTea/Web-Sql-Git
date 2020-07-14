import { Component, OnInit, HostListener } from '@angular/core';
import { UserService } from '../../authentication/shared/user.service';
import { User } from '../../authentication/shared/User';
import { Script } from '../../scripts/shared/Script';
import { Observable, Subject } from 'rxjs';
import {
  debounceTime, distinctUntilChanged, switchMap
} from 'rxjs/operators';
import { ScriptService } from '../../scripts/shared/script.service';
import { Key } from 'protractor';
import { SCRIPTS } from '../../scripts/shared/SCRIPTS';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  user = <User>{};
  scripts: Script[] = null;
  isExpanded = false;
  isAurorize = true;
  private searchTerms = new Subject<string>();

  constructor(private userService: UserService, private scriptService: ScriptService) { }

  ngOnInit(): void {
    this.getUser();
  }

  @HostListener('document:click')
  clickout() {
    this.scripts = null;
  }
  

  search(term: string): void {
    this.searchTerms.next(term);
    /*this.scripts = this.searchTerms.pipe(
      switchMap((term: string) => this.scriptService.searchScript(term)));*/

    this.scripts = SCRIPTS;

  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  Out() {
    this.userService.outUser().subscribe(() => window.location.href = "/scripts" );
  }

  Log() {
    window.location.href = "/login"
  }

  getUser() {
    this.userService.getUser()
      .subscribe(user => {
        if (user.name == null) {
          this.isAurorize = false;
        } else {
          this.user = user;}
      });
  }
}
