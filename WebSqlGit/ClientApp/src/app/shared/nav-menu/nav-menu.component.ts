import { Component, OnInit } from '@angular/core';
import { UserService } from '../../authentication/shared/user.service';
import { User } from '../../authentication/shared/User';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  user = <User>{ };
  isExpanded = false;
  isAurorize = true;

  constructor(private userService: UserService) {}
  ngOnInit(): void {
    this.getUser();
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
