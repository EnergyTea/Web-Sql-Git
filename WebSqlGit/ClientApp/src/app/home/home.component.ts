import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  name = 'Любимое пиво';
  beer = "9";

  public newBeer() {
    this.beer = (<HTMLInputElement>document.getElementById("11")).value;
  }

}
