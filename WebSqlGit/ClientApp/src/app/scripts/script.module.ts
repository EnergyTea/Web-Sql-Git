import { NgModule } from '@angular/core';
import { RouterModule, Routes, Router } from '@angular/router';

import { ScriptsComponent } from './scripts/scripts.component';
import { CreateComponent } from './create/create.component';
import { ScriptComponent } from './script/script.component';
import { CategoriesComponent } from './categories/categories.component';
import { OneCategoryComponent } from './one-category/one-category.component';

const scriptRoutes: Routes = [
  { path: 'create', component: CreateComponent },
  { path: ':id', component: ScriptComponent }
]

const scriptsRoutes: Routes = [
  { path: 'scripts', component: ScriptsComponent, children: scriptRoutes },
]

const categoryRoutes: Routes = [
  {path: ':id', component: OneCategoryComponent, children: scriptsRoutes}
]

const routes: Routes = [
  { path: '', redirectTo: '/scripts', pathMatch: 'full' },
  { path: 'scripts', component: ScriptsComponent},
  { path: 'category', component: CategoriesComponent, children: categoryRoutes}
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class ScriptModule {
}
