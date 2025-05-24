import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import Employee from '@app/shared/models/companies/employee';
import EmployeeForEdit from '@app/shared/models/companies/employeeForEdit';
import QueryParameters from '@app/shared/models/params/QueryParameters';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  #apiBaseUrl : string = 'https://localhost:7192/api/companies';

  constructor(private http : HttpClient) { }

  getEmployeesForCompany(companyId : string, queryParams : QueryParameters) : 
    Observable<HttpResponse<Employee[]>>
  {
    const completeFetchUrl : string = 
      `${this.#apiBaseUrl}/${companyId}
      /employees?pageNumber=${queryParams.pageNumber}
      &pageSize=${queryParams.pageSize}
      &orderBy=${queryParams.orderBy}
      &searchTerm=${queryParams.searchTerm}`;

      console.log(completeFetchUrl);

    const result : Observable<HttpResponse<Employee[]>> = 
       this.http.get<Employee[]>(completeFetchUrl, { observe: 'response'});

    return result;
  }

  saveEmployeeForCompany(companyId: string, employeeId: string, updateBody : EmployeeForEdit){
    const completePutUrl : string = 
      `${this.#apiBaseUrl}/${companyId}/employees/${employeeId}`;

    const result : Observable<Object> = this.http.put(completePutUrl, updateBody);

    return result;
  }
}
