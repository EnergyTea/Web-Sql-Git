import { Location } from '@angular/common';
import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { User } from '../shared/User';
import { UserService } from '../shared/user.service';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

/** login component*/
export class LoginComponent {
  /** login ctor */
  constructor(
    private userService: UserService,
    private location: Location
  ) { }

  user: User;

  reg(): void {
    window.location.href = "/registration"; 
  }

  loginIn(user: User, IngressModul: NgForm) {
    user = <User>{};
    user.login = IngressModul.value.email;
    user.password = IngressModul.value.password;
    if (user.login !== "" && user.password !== "") {
      this.userService.authorizationUser(user).subscribe(() => window.location.href = "/scripts");
    }
  }
}
