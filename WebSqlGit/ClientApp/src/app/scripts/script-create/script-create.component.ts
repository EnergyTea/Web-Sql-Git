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
  categories: Category[];
  open: boolean = false;
  tags: string[] = [];

  constructor(
    private scriptService: ScriptService,
    private categoryService: CategoryService
  ) { }
  
  createTags(tagNew: string) {
    tagNew = tagNew.trim();
    if (tagNew !== "") {
      this.tags.push(tagNew);
    }
  }
  deleteTag(tag) {
    console.log("id: ", tag);
    delete this.tags[tag];
  }

  ngOnInit() {
    this.getCategory();
  }

  getCategory(): void {
    this.categoryService.getCategories()
      .subscribe(categories => this.categories = categories)
  }

  add(newScript: Script, create: NgForm) {
    newScript = <Script>{};
    newScript.name = create.value.name;
    newScript.body = create.value.body;
    newScript.tags = this.tags;
    newScript.categoryId = Number(create.value.categoryId);
    this.scriptService.addScript(newScript)
      .subscribe(script => this.scripts.push(script));
    window.location.href = "/scripts";
  }
}
