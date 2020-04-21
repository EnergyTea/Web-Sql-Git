import { Component, OnInit } from '@angular/core';
import { CATEGORIES } from '../shared/category/mock-categories';
import { Category } from '../shared/category/Category';

@Component({
    selector: 'app-scripts',
    templateUrl: './scripts.component.html',
    styleUrls: ['./scripts.component.css']
})
/** scripts component*/
export class ScriptsComponent implements OnInit {
    /** scripts ctor */
  categories = CATEGORIES;
  selectedCategory: Category;

  ngOnInit() { }

  off(): void {
    this.selectedCategory = null
  }

  onSelect(category: Category): void {
    this.selectedCategory = category;
  }
}
