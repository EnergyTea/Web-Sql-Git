import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ScriptModule } from './scripts/script.module';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './shared/nav-menu.component'
import { ScriptsComponent } from './scripts/scripts/scripts.component';
import { CreateComponent } from './scripts/create/create.component';
import { ScriptComponent } from './scripts/script/script.component';
import { CategoryComponent } from './shared/category/category.component';
import { CategoriesComponent } from './scripts/categories/categories.component';
import { OneCategoryComponent } from './scripts/one-category/one-category.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    CategoryComponent,
    CategoriesComponent,
    CreateComponent,
    ScriptComponent,
    ScriptsComponent,
    OneCategoryComponent
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
