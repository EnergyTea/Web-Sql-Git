import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { User } from '../shared/User';
import { UserService } from '../shared/user.service';
import { Location } from '@angular/common';

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
    private location: Location) {

  }

  goList(): void {
    this.location.prepareExternalUrl('scripts');
  }

  loginIn(user: User, IngressModul: NgForm) {
    user = <User>{};
    user.login = IngressModul.value.email;
    user.password = IngressModul.value.password;
    this.userService.authorizationUser(user).subscribe(() => this.goList)
  }
}
