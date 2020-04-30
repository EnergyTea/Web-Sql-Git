import { Component, OnInit } from '@angular/core';
import { Category } from '../shared/category/Category';
import { CategoryService } from '../shared/category/category.service';


@Component({
  selector: 'app-scripts',
  templateUrl: './scripts.component.html',
  styleUrls: ['./scripts.component.css']
})
/** scripts component*/
export class ScriptsComponent implements OnInit {
/** scripts ctor */


  categories: Category[];
  selectedCategory: Category;
  created: boolean;
  constructor(private categoryService: CategoryService) { }


  ngOnInit() {
    this.getCategory();
  }

  getCategory(): void {
    this.categoryService.getCategory()
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

  off(): void {
    this.selectedCategory = null
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
