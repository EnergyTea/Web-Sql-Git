import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ScriptModule } from './scripts/script.module';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './shared/nav-menu.component'
import { ScriptsComponent } from './scripts/scripts/scripts.component';
import { EditComponent } from './scripts/edit/edit.component';
import { CreateComponent } from './scripts/create/create.component';
import { ScriptComponent } from './scripts/script/script.component';
import { ScriptListComponent } from './scripts/shared/script-list/script-list.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    CreateComponent,
    EditComponent,
    ScriptComponent,
    ScriptListComponent,
    ScriptsComponent,
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
