import { Component, OnInit } from '@angular/core';
import { User } from '../shared/User';
import { UserService } from '../shared/user.service';

@Component({
    selector: 'registration',
    templateUrl: './registration.component.html',
    styleUrls: ['./registration.component.css']
})

/** registration component*/
export class RegistrationComponent implements OnInit {
/** registration ctor */
  users: User[] = [];
  user = <User>{};
  isBusy = false;

  constructor(private userService: UserService) { }

  ngOnInit(): void {
  }

  registrationUser(user: User): void {
    user = <User>{};
    user.name = this.user.name;
    user.login = this.user.login;
    user.password = this.user.password;
    if (user.name !== "" && user.password !== "" && user.login !== "") {
      this.userService.createUser(user).subscribe(() => window.location.href = "/login")
    } else { this.isBusy = true;}
  }
}
