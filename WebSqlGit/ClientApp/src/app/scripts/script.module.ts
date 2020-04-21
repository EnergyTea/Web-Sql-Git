import { NgModule } from '@angular/core';
import { RouterModule, Routes, Router } from '@angular/router';

import { ScriptsComponent } from './scripts/scripts.component';
import { CreateComponent } from './create/create.component';
import { EditComponent } from './edit/edit.component';
import { ScriptComponent } from './script/script.component';
import { ScriptListComponent } from './shared/script-list/script-list.component';

const scriptRoutes: Routes = [
  { path: 'list', component: ScriptListComponent},
  { path: 'edit', component: EditComponent },
  { path: 'create', component: CreateComponent },
  { path: ':id', component: ScriptComponent }
]

const routes: Routes = [
  { path: '', redirectTo: '/scripts/list', pathMatch: 'full' },
  { path: 'scripts', redirectTo: '/scripts/list', pathMatch: 'full' },
  { path: 'scripts', component: ScriptsComponent},
  { path: 'scripts', component: ScriptsComponent, children: scriptRoutes}
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class ScriptModule {
}
