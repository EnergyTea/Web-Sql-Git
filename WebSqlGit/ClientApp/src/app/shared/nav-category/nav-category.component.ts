import { Component, OnInit } from '@angular/core';
import { Category } from '../../categories/shared/Category';
import { CategoryService } from '../../categories/shared/category.service';

@Component({
    selector: 'nav-category',
    templateUrl: './nav-category.component.html',
    styleUrls: ['./nav-category.component.css']
})
/** category component*/
export class NavCategoryComponent implements OnInit {
    /** category ctor */
  categories: Category[];
  created: boolean;
  isAurorize = false;
  public highlightedDiv: number;

  constructor(private categoryService: CategoryService) { }

  public toggleHighlight(newValue: number) {
    this.highlightedDiv = newValue;
  }

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
      .subscribe(data => this.getCategory());
    this.getCategory();
    window.location.reload();
  }

  add(name: string): void {
    name = name.trim();
    if (!name) { return; }
    if (!this.categories.map(item => item.name).includes(name)) {
      this.categoryService.addCategory({ name } as Category)
        .subscribe(category => { this.categories.push(category) });
      this.getCategory();
      window.location.href = "/scripts";
    }
  }

  crate(): void {
    this.created = !this.created
  }
}
