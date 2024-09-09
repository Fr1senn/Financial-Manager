import { Component, inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Category } from '../../../../../../models/category';
import { OperationResult } from '../../../../../../models/operation-result';
import { MatDialogRef } from '@angular/material/dialog';
import { ICategoryService } from '../../../../../../services/interfaces/category.interface';
import { CategoryService } from '../../../../../../services/category.service';

@Component({
  selector: 'app-category-creation',
  templateUrl: './category-creation.component.html',
  styleUrl: './category-creation.component.css',
})
export class CategoryCreationComponent implements OnInit {
  public categoryCreationForm: FormGroup | undefined;

  private dialogRef = inject(MatDialogRef<CategoryCreationComponent>);

  private readonly categoryService: ICategoryService = inject(CategoryService);

  public ngOnInit(): void {
    this.categoryCreationForm = this.createCategoryCreationForm();
  }

  public createCategory() {
    const category: Category = new Category(
      this.categoryCreationForm?.value.title
    );

    this.categoryService
      .createCategory(category)
      .subscribe((response: OperationResult) => {
        if (response.isSuccess) {
          this.dialogRef.close();
        }
      });
  }

  private createCategoryCreationForm(): FormGroup {
    return new FormGroup({
      title: new FormControl('', Validators.required),
    });
  }
}
