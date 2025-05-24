import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-new-company-form-modal', 
  standalone: true,
  imports: [MatDialogModule, ReactiveFormsModule, MatFormFieldModule, MatInputModule, CommonModule],
  templateUrl: './new-company-form-modal.component.html',
  styleUrl: './new-company-form-modal.component.css'
})
export class NewCompanyFormModalComponent {
  form!: FormGroup;

  constructor(private dialog : MatDialogRef<NewCompanyFormModalComponent>){
    this.form = new FormGroup({
      name: new FormControl(''),
      address: new FormControl(''),
      country: new FormControl('')
    });
  }

  submit() {
    if (this.form.valid) {
      this.dialog.close(this.form.value); // return form data
    }
  }

  close() {
    this.dialog.close();
  }
}
