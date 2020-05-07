import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ScriptModule } from './scripts/script.module';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './shared/nav-menu/nav-menu.component';
import { NavCategoryComponent } from './shared/nav-category/nav-category.component';
import { CategoryComponent } from './categories/category/category.component';
import { EditScriptComponent } from './scripts/script-edit/script-edit.component';
import { ScriptComponent } from './scripts/script-view/script.component';
import { ScriptsComponent } from './scripts/script-list/script-list.component';
import { CreateScriptComponent } from './scripts/script-create/script-create.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    NavCategoryComponent,
    CategoryComponent,
    ScriptComponent,
    ScriptsComponent,
    CreateScriptComponent,
    EditScriptComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ScriptModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
