import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { ScriptModule } from './scripts/script.module';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './shared/nav-menu/nav-menu.component';
import { NavCategoryComponent } from './shared/nav-category/nav-category.component';
import { CategoryComponent } from './categories/category/category.component';
import { EditScriptComponent } from './scripts/script-edit/script-edit.component';
import { ScriptComponent } from './scripts/script-view/script.component';
import { ScriptsComponent } from './scripts/script-list/script-list.component';
import { CreateScriptComponent } from './scripts/script-create/script-create.component';
import { LoginComponent } from './authentication/login/login.component';
import { RegistrationComponent } from './authentication/registration/registration.component';
import { DirectionComponent } from './shared/direction/direction.component';
import { ScriptUserComponent } from './scripts/script-user/script-user.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    NavCategoryComponent,
    DirectionComponent,
    ScriptUserComponent,
    CategoryComponent,
    ScriptComponent,
    ScriptsComponent,
    CreateScriptComponent,
    EditScriptComponent,
    LoginComponent,
    RegistrationComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ScriptModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})

export class AppModule { }
