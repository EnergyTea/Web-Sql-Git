import { Component, OnInit } from '@angular/core';
import { Script } from '../shared/Script';
import { Category } from '../../categories/shared/Category';
import { ScriptService } from '../shared/script.service';
import { NgForm } from '@angular/forms';
import { CategoryService } from '../../categories/shared/category.service';

@Component({
  selector: 'app-create',
  templateUrl: './script-create.component.html',
  styleUrls: ['./script-create.component.css']
})

/** create component*/
export class CreateScriptComponent implements OnInit {
  /** create ctor */
  scripts: Script[];
  categories: Category[] = [] ;
  open = false;
  isError = false;
  tags: string[] = [];
  selectedCategory: number;

  constructor(
    private scriptService: ScriptService,
    private categoryService: CategoryService
  ) { }

  ngOnInit() {
    this.getCategory();
  }

  createTags(tagNew: string) {
    tagNew = tagNew.trim();
    if (tagNew !== "") {
      this.tags.push(tagNew);
    }
  }
  deleteTag(i: number) {
    this.tags.splice(i, 1);
  }

  getCategory(): void {
    this.categoryService.getCategories()
      .subscribe(categories => this.categories = categories)
  }

  addScript(newScript: Script, create: NgForm) {
    newScript = <Script>{};
    newScript.name = create.value.name;
    newScript.body = create.value.body;
    newScript.tags = this.tags;
    newScript.categoryId = this.selectedCategory;
    if (newScript.name !== "" && newScript.body !== "" && newScript.categoryId !== undefined) {
      this.scriptService.addScript(newScript)
        .subscribe(script => this.scripts.push(script));
      window.location.href = "/scripts"
    } else { this.isError = true}
  }
}
