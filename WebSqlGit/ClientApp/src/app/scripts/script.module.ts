import { NgModule } from '@angular/core';
import { RouterModule, Routes, Router } from '@angular/router';
import { CategoryComponent } from '../categories/category/category.component';
import { EditScriptComponent } from './script-edit/script-edit.component';
import { ScriptComponent } from './script-view/script.component';
import { ScriptsComponent } from './script-list/script-list.component';
import { CreateScriptComponent } from './script-create/script-create.component';



const routes: Routes = [
  { path: '', redirectTo: '/scripts', pathMatch: 'full' },
  { path: 'scripts', component: ScriptsComponent },
  { path: 'scripts/create', component: CreateScriptComponent },
  { path: 'category/:CategoryId', component: CategoryComponent },
  { path: 'scripts/:ScriptId', component: ScriptComponent },
  { path: 'scripts/:ScriptId/edit', component: EditScriptComponent },
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class ScriptModule {
}
