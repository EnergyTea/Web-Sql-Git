import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../../authentication/shared/user.service';
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
  public highlightedDiv: number;
  isEditCategory: boolean;
  isAurorize = true;

  constructor(
    private categoryService: CategoryService,
    private userService: UserService) { }

  ngOnInit() {
    this.getCategory();
    this.getUser();
  }

  getValueAtIndex(index) {
    var str = window.location.href;
    return str.split("/")[index];
  }

  getCategory(): void { 
    this.categoryService.getCategories()
      .subscribe(categories => {
        this.categories = categories;
        if ((Number(this.getValueAtIndex(4) !== null) && (this.getValueAtIndex(3) == 'category'))) {
          this.highlightedDiv = Number(this.getValueAtIndex(4));
        } else { this.highlightedDiv = 0 }
      });
  }

  getUser() {
    this.userService.getUser()
      .subscribe(user => {
        if (user.name == null) {
          this.isAurorize = false;
        }
      });
  }

  addCategory(name: string): void {
    name = name.trim();
    if (!name) {
      return;
    }
    if (!this.categories.map(item => item.name).includes(name)) {
      this.categoryService.addCategory({ name } as Category)
        .subscribe(category => { this.categories.push(category) });
      this.getCategory();
      window.location.href = "/scripts";
    }
  }
}
