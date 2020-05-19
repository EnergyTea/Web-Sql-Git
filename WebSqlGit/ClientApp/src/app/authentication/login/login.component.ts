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
  private userService: UserService) {

  }

  loginIn(user: User, IngressModul: NgForm) {
    user = <User>{};
    user.login = IngressModul.value.email;
    user.password = IngressModul.value.password;
    this.userService.authorizationUser(user).subscribe()
  }
}