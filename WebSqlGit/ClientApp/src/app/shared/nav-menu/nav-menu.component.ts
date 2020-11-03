import { Component, HostListener, OnInit } from '@angular/core';
import { User } from '../../authentication/shared/User';
import { UserService } from '../../authentication/shared/user.service';
import { Script } from '../../scripts/shared/Script';
import { ScriptService } from '../../scripts/shared/script.service';
import { Router } from '@angular/router';

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

  constructor(
    private router: Router,
    private userService: UserService,
    private scriptService: ScriptService
  ) { }

  ngOnInit(): void {
    this.getUser();
  }

  @HostListener('document:click')
  clickout() {
    this.scripts = null;
  }  

  search(pattern: string): void {
    this.scriptService.searchScript(pattern)
      .subscribe(scripts => this.scripts = scripts.reverse());
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logitOut() {
    this.userService.outUser().subscribe(() => window.location.href = "/scripts" );
  }

  getUser() {
    this.userService.getUser()
      .subscribe(user => {
        if (user.name == null) {
          this.isAurorize = false;
        } else {
          this.user = user;
        }
      });
  }
}
