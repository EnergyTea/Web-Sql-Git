import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ProductComponent } from './Product/product.component';
import { EditComponent } from './edit/edit-code.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ProductComponent,
    EditComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'app-product', component: ProductComponent },
      { path: 'app-edit', component: EditComponent },
    ])
  ],
  providers: [],
  bootstrap: [
    AppComponent,
    HomeComponent,
    NavMenuComponent,
    ProductComponent,
    EditComponent
  ]
})
export class AppModule { }
