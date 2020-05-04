import { Component, OnInit } from '@angular/core';
import { Category } from '../../scripts/shared/category/Category';
import { CategoryService } from '../../scripts/shared/category/category.service';

@Component({
    selector: 'category',
    templateUrl: './category.component.html',
    styleUrls: ['./category.component.css']
})
/** category component*/
export class CategoryComponent implements OnInit {
    /** category ctor */
  categories: Category[];
  selectedCategory: Category;
  created: boolean;
  constructor(private categoryService: CategoryService) { }


  ngOnInit() {
    this.getCategory();
  }

  getCategory(): void {
    this.categoryService.getCategories()
      .subscribe(categories => this.categories = categories)
  }

  delete(category: Category): void {
    this.categories = this.categories.filter(c => c !== category);
    this.categoryService.deleteCategory(category.id)
      .subscribe(data => this.getCategory())
  }

  add(name: string): void {
    name = name.trim();
    if (!name) { return; }
    this.categoryService.addCategory({ name } as Category)
      .subscribe(category => { this.categories.push(category) })
  }

  crate(): void {
    this.created = !this.created
  }
  //getScripts(): Script[] {
  //  //return scriptService.getScripts();
  //}

  onSelect(category: Category): void {
    this.selectedCategory = category;
  }
}
