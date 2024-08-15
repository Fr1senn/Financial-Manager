import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { provideHttpClient, withFetch } from '@angular/common/http';
@NgModule({
  declarations: [CategoryCreationComponent],
  imports: [
    CommonModule,
  ],
  providers: [provideHttpClient(withFetch())],
})
export class CategoryModule {}
