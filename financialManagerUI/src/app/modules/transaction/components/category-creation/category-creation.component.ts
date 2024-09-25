import { Component, inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { CategoryRequest } from '../../../../entities/requests/category.request';
import { ICategoryService } from '../../services/interfaces/category.interface';
import { CategoryService } from '../../services/category.service';
import { catchError, tap, throwError } from 'rxjs';

@Component({
  selector: 'app-category-creation',
  templateUrl: './category-creation.component.html',
  styleUrl: './category-creation.component.css',
})
export class CategoryCreationComponent implements OnInit {
  private readonly categoryCreationDialogRef: MatDialogRef<CategoryCreationComponent> =
    inject(MatDialogRef<CategoryCreationComponent>);
  private readonly _categoryService: ICategoryService = inject(CategoryService);

  public categoryCreationForm: FormGroup | undefined;
  public errorMessage: string = '';

  public ngOnInit(): void {
    this.categoryCreationForm = this.createCategoryCreationForm();
  }

  public closeDialog() {
    this.categoryCreationDialogRef.close();
  }

  public createCategory(): void {
    const request: CategoryRequest = new CategoryRequest(
      this.categoryCreationForm?.value.title
    );

    this._categoryService
      .createCategory(request)
      .pipe(
        tap((res) => {
          if (res.isSuccess) {
            this.closeDialog();
          }
        }),
        catchError((error) => throwError(() => error))
      )
      .subscribe();
  }

  private createCategoryCreationForm(): FormGroup {
    return new FormGroup({
      title: new FormControl('', [
        Validators.required,
        Validators.minLength(5),
      ]),
    });
  }
}
