import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Category } from '../../categories/shared/Category';
import { CategoryService } from '../../categories/shared/category.service';
import { Script } from '../shared/Script';
import { ScriptService } from '../shared/script.service';

@Component({
    selector: 'app-edit',
    templateUrl: './script-edit.component.html',
    styleUrls: ['./script-edit.component.css']
})

/** edit component*/
export class EditScriptComponent implements OnInit {
/** edit ctor */
  scripts: Script[];
  categories: Category[];
  script = <Script>{};
  tags: string[] = [];
  isError = false;
  selectedCategory: number;

  constructor(
    private scriptService: ScriptService,
    private categoryService: CategoryService,
    private route: ActivatedRoute,
    private scriptScrvice: ScriptService,
    private location: Location,
  ) {
    this.getScript();
  }

  ngOnInit(): void {
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

  async getScript() {
    const ScriptId = + this.route.snapshot.paramMap.get('ScriptId');
    this.scriptScrvice.getScript(ScriptId)
      .subscribe(script => {
      this.script = script;
        this.tags = script.tags;
      });
  }

  async getCategory() {
    this.categoryService.getCategories()
      .subscribe(categories => {
        this.categories = categories;
        categories.forEach(item => {
          if (item.id == this.script.categoryId) {
            this.selectedCategory = item.id;
          }
        });
      })
  }

  goBack(): void {
    this.location.back();
  }

  upDate(newScript: Script) {
    newScript = <Script>{};
    newScript.scriptId = this.script.scriptId;
    newScript.name = this.script.name;
    newScript.body = this.script.body;
    newScript.tags = this.tags;
    this.selectedCategory == undefined ? newScript.categoryId = this.script.categoryId : newScript.categoryId = this.selectedCategory;   
    if (newScript.name !== "" && newScript.body !== "") {
      this.scriptService.upDateScript(newScript)
        .subscribe(script => this.scripts.push(script))
      this.goBack();
    } else {
      this.isError = true
    }
  }
}
