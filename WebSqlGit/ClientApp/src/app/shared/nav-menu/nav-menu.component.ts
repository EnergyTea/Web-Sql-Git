import { Component, OnInit } from '@angular/core';
import { UserService } from '../../authentication/shared/user.service';
import { User } from '../../authentication/shared/User';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  user: User;
  isExpanded = false;

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
    this.userService.outUser().subscribe();
  }

  getUser() {
    this.userService.getUser()
      .subscribe(user => { this.user = user; console.log(user) })
    console.log("ahaha", this.user.name)
  }
}
