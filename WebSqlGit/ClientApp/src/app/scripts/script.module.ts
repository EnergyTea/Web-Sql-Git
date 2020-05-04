import { NgModule } from '@angular/core';
import { RouterModule, Routes, Router } from '@angular/router';
import { ScriptsComponent } from './scripts/scripts.component';
import { CategoryComponent } from '../categories/category/category.component';
import { CreateScriptComponent } from './create-script/create-script.component';
import { EditScriptComponent } from './edit-script/edit-script.component';
import { ScriptComponent } from './script-view/script.component';



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
