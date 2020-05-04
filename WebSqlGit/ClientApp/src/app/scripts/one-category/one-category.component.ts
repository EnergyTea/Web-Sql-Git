import { Component, OnInit } from '@angular/core';
import { Category } from '../shared/category/Category';
import { ActivatedRoute } from '@angular/router';
import { CategoryService } from '../shared/category/category.service';

@Component({
    selector: 'one-category',
    templateUrl: './one-category.component.html',
    styleUrls: ['./one-category.component.css']
})
/** one-category component*/
export class OneCategoryComponent implements OnInit {
/** one-category ctor */
  category: Category;

  constructor(
    private route: ActivatedRoute,
    private categoryService: CategoryService
  ) { }

  ngOnInit() {
    this.getCategory();
  }

  getCategory(): void {
    const id = + this.route.snapshot.paramMap.get('id');
    this.categoryService.getCategory(id)
      .subscribe(category => this.category = category)
  }

}
