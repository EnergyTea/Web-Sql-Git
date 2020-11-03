import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Script } from '../shared/Script';
import { ScriptService } from '../shared/script.service';
import { Category } from '../../categories/shared/Category';
import { CategoryService } from '../../categories/shared/category.service';


@Component({
  selector: 'app-scripts',
  templateUrl: './script-list.component.html',
  styleUrls: ['./script-list.component.css']
})

/** scripts component*/
export class ScriptsComponent implements OnInit {
  scripts: Script[];
  categories: Category[];

  constructor(
    private router: Router,
    private scriptService: ScriptService,
    private categoryService: CategoryService
  ) { }

  ngOnInit() {
    this.getScripts();
  }

  getScripts(): void {
    this.scriptService.getScripts()
      .subscribe(scripts => this.scripts = scripts.reverse());
    this.categoryService.getCategories()
      .subscribe(categories => this.categories = categories);
  }

  goTo(id: number): void {
    this.router.navigate(['scripts/' + id]);
  }
}
