import { Component, OnInit } from '@angular/core';
import { UserService } from '../shared/user.service';
import { User } from '../shared/User';
import { NgForm } from '@angular/forms';

@Component({
    selector: 'registration',
    templateUrl: './registration.component.html',
    styleUrls: ['./registration.component.css']
})
/** registration component*/
export class RegistrationComponent implements OnInit {
/** registration ctor */
  users: User[] = [];
  constructor(private userService: UserService) { }
    ngOnInit(): void {
        this.getAll();
    }

  Registration(user: User, registModul: NgForm): void {
    user = <User>{};
    user.name = registModul.value.name;
    user.login = registModul.value.email;
    user.password = registModul.value.password;
    console.log(this.users.map(item => item.login));
    if (!this.users.map(item => item.login).includes(user.login) && user.name !== "" && user.password !== "" && user.login !== "") {
      this.userService.createUser(user).subscribe(() => window.location.href = "/scripts")
    }
  }

  getAll(): void {
    this.userService.getUsers()
      .subscribe(users => this.users = users);
  }
}
