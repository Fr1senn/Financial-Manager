import { Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Injectable({
  providedIn: 'root',
})
export class TransactionCreationFormValidatorService {
  public createForm(): FormGroup {
    return new FormGroup({
      title: new FormControl('', [Validators.required]),
      significance: new FormControl(1, [
        Validators.required,
        Validators.min(1),
      ]),
      transactionType: new FormControl('', Validators.required),
    });
  }
}
