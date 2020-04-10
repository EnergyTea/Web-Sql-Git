import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { SharedModule } from './shared/shared.module';
import { EditComponent } from './scripts/edit/edit.component';
import { ScriptComponent } from './scripts/script/script.component';
import { UsersComponent } from './users/users.component';
import { ScriptsComponent } from './scripts/shared/scripts.component';
import { CreateComponent } from './scripts/create/create.component';


const itemRoutes: Routes = [
  { path: 'scr', component: ScriptComponent, pathMatch: 'full'  },
  { path: 'edit', component: EditComponent },
  { path: 'create', component: CreateComponent },
];

@NgModule({
  declarations: [
    AppComponent,
    EditComponent,
    ScriptsComponent,
    ScriptComponent,
    UsersComponent,
    CreateComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    SharedModule,
    RouterModule.forRoot([
      { path: '', component: UsersComponent },
      { path: 'scripts', component: ScriptsComponent },
      { path: 'scripts', component: ScriptsComponent, children: itemRoutes },
    ])
  ],
  providers: [],
  bootstrap: [
    AppComponent,
    EditComponent
  ]
})
export class AppModule { }
