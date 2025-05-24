import { Component, input, InputSignal, OnInit, Output, output } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import Employee from '@app/shared/models/companies/employee';
import EmployeeForEdit from '@app/shared/models/companies/employeeForEdit';

@Component({
  selector: 'app-employee-card',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './employee-card.component.html',
  styleUrl: './employee-card.component.css'
})
export class EmployeeCardComponent implements OnInit{
  isEditing: boolean = false;
  editForm!: FormGroup;
  employee : InputSignal<Employee | undefined> = input<Employee>();
  onSaveEdit = output<{employeeId: string, updateBody: EmployeeForEdit}>();

  ngOnInit(): void {
    this.editForm = new FormGroup({
      name: new FormControl(this.employee()?.name),
      position: new FormControl(this.employee()?.position),
      age: new FormControl(this.employee()?.age)
    })
  }

  enableEditing() : void {
    this.isEditing = true;
  }

  saveEdit() : void{
    const updateData = { 
      employeeId: this.employee()!.id, 
      updateBody: this.editForm.value
    };
    this.isEditing = false;
    this.onSaveEdit.emit(updateData);
  }

  cancelEditing() : void {
    this.isEditing = false;
  }
}
