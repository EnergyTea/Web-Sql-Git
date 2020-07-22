import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Script } from '../../scripts/shared/Script';
import { Category } from '../shared/Category';
import { CategoryService } from '../shared/category.service';

@Component({
    selector: 'category',
    templateUrl: './category.component.html',
    styleUrls: ['./category.component.css']
})

/** category component*/
export class CategoryComponent implements OnInit {
    /** category ctor */
  category = <Category>{};
  scripts: Script[];
  @Input() CategoryId: number;

  constructor(
    private route: ActivatedRoute,
    private categoryService: CategoryService
  ) { }

  ngOnInit() {
    this.getCategory();
    this.getScripts();
  }

  getCategory(): void {
    this.CategoryId = + this.route.snapshot.paramMap.get('CategoryId');
    this.categoryService.getCategory(this.CategoryId)
      .subscribe(category => this.category = category)
  }

  getScripts(): void {
    const CategoryId = + this.route.snapshot.paramMap.get('CategoryId');
    this.categoryService.getScripts(CategoryId)
      .subscribe(scripts => this.scripts = scripts)
  }
}
