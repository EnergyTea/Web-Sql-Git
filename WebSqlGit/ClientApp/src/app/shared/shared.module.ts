import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { NavComponent } from './nav/nav-menu.component';
@NgModule({
  imports: [BrowserModule, FormsModule],
  declarations: [NavComponent],
  exports: [NavComponent]       // экспортируем компонент
})
export class SharedModule {
}
