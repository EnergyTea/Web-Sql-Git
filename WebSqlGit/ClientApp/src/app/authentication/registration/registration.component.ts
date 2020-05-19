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
  constructor(private userService: UserService) { }
    ngOnInit(): void {
        throw new Error("Method not implemented.");
    }

  Registration(user: User, registModul: NgForm): void {
    user = <User>{};
    user.name = registModul.value.name;
    user.login = registModul.value.email;
    user.password = registModul.value.password;
    this.userService.createUser(user).subscribe()
    console.log(user)
  }
}
