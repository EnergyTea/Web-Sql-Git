import { Component, OnInit, Input, Output } from '@angular/core';
import { Category } from '../../categories/shared/Category';
import { CategoryService } from '../../categories/shared/category.service';
import { UserService } from '../../authentication/shared/user.service';
import { ActivatedRoute } from '@angular/router';
import { CategoryComponent } from '../../categories/category/category.component';

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
  isAurorize = true;
  public highlightedDiv: number;

  constructor(
    private categoryService: CategoryService,
    private userService: UserService,  
    private route: ActivatedRoute,) { }

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
        if ((Number(this.getValueAtIndex(4) !== null) && (this.getValueAtIndex(3) == 'category')) ) {
          this.highlightedDiv = Number(this.getValueAtIndex(4));
        }
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
