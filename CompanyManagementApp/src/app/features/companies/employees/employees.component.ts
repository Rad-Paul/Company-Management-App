import { Component, OnInit } from '@angular/core';
import Employee from '@app/shared/models/companies/employee';
import { ActivatedRoute } from '@angular/router';
import { EmployeeService } from '@app/services/employee/employee.service';
import { EmployeeCardComponent } from '@app/shared/components/employee-card/employee-card.component';
import { RouterLink, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import EmployeeForEdit from '@app/shared/models/companies/employeeForEdit';
import QueryParameters from '@app/shared/models/params/QueryParameters';

@Component({
  selector: 'app-employees',
  standalone: true,
  imports: [EmployeeCardComponent, RouterLink, FormsModule],
  templateUrl: './employees.component.html',
  styleUrl: './employees.component.css'
})
export class EmployeesComponent implements OnInit{
  orderBy : string | undefined;
  searchTerm: string | null = '';
  companyId : string | undefined;
  pageNumber : number | undefined;
  pageSize : number | undefined;
  employees : Employee[] | undefined;
  pages : number[] | undefined;

  constructor(private route: ActivatedRoute, private router: Router, private employeeService : EmployeeService){

  }

  ngOnInit(): void {
    this.companyId = this.route.snapshot.paramMap.get('companyId')!;

    this.route.queryParamMap.subscribe(queryParams => {
      this.orderBy = queryParams.get('orderBy') ?? "name";
      this.searchTerm = queryParams.get('searchTerm');
      this.pageNumber = Number(queryParams.get('pageNumber')) ?? 1;
      this.pageSize = Number(queryParams.get('pageSize')) ?? 5;
      this.loadEmployeeData();
    })

  }
  
  loadEmployeeData() : void{
      const response = this.employeeService.getEmployeesForCompany(this.companyId!, this.getQueryParams());

      response.subscribe({
        next: (response) => {
          console.log('data fetched')
          this.employees = [...(response.body ?? [])];
          const paginationData = JSON.parse(response.headers.get('X-Pagination')!);
          
          this.pages = Array.from({ length: paginationData.TotalPages }, (_, i) => i + 1)
        },
        error: (err) => {
          console.error('failed to get pagination data!', err);
        }
      })
  }

  saveEmployee(data: {employeeId : string, updateBody : EmployeeForEdit}) : void{
    this.employeeService.saveEmployeeForCompany(this.companyId!, data.employeeId, data.updateBody)
      .subscribe({
        next: (response) => {
          console.log('Updated employee succesfully')
          this.loadEmployeeData();
        },
        error: (err) => {
          console.error('Failed to update employee', err)
        }
      });
  }

  onSortChange() : void{
    console.log('sort changed: ' + this.orderBy)
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: this.getQueryParams()
    })
  }

  onSearchChange() : void{
    console.log('search changed to:' + this.searchTerm)
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: this.getQueryParams()
    })
  }

  getQueryParams() : QueryParameters{ 

    return new QueryParameters(
      this.pageNumber ?? 1,
      this.pageSize ?? 5,
      this.orderBy ?? 'name',
      this.searchTerm
    );  
    
  }

}
