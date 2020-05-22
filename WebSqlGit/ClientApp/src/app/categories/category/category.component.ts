import { Component, OnInit } from '@angular/core';
import { Category } from '../shared/Category';
import { ActivatedRoute } from '@angular/router';
import { CategoryService } from '../shared/category.service';
import { Script } from '../../scripts/shared/Script';

@Component({
    selector: 'category',
    templateUrl: './category.component.html',
    styleUrls: ['./category.component.css']
})
/** category component*/
export class CategoryComponent implements OnInit {
    /** category ctor */
  category: Category;
  scripts: Script[];

  constructor(
    private route: ActivatedRoute,
    private categoryService: CategoryService
  ) { }

  ngOnInit() {
    this.getCategory();
    this.getScripts();
  }

  getCategory(): void {
    const CategoryId = + this.route.snapshot.paramMap.get('CategoryId');
    console.log("321", CategoryId)
    this.categoryService.getCategory(CategoryId)
      .subscribe(category => this.category = category)
  }

  getScripts(): void {
    const CategoryId = + this.route.snapshot.paramMap.get('CategoryId');
    this.categoryService.getScripts(CategoryId)
      .subscribe(scripts => this.scripts = scripts)
  }
}
