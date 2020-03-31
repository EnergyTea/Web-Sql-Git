import { Component } from '@angular/core';

@Component({
    selector: 'app-test-input',
    templateUrl: './testInput.html',
})

export class TestInput {
  name = 'Egor';
  public test = true;
  Day = new Date().getDate();
  Month = new Date().getMonth();
  Year = new Date().getFullYear();
  Hours = new Date().getHours();
  Minutes = new Date().getMinutes();

  public saveCode() {
    this.test = !this.test;
  }
}
