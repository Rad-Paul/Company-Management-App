import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import CompanyForCreation from '@app/shared/models/companies/companyForCreation';
import Company from '@shared/models/companies/company';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {
  apiCompanyRepoUrl : string = 'https://localhost:7192/api/companies';
  #http : HttpClient = inject(HttpClient);

  constructor() { }

  getAllCompanies() : Observable<Company[]>{
    return this.#http.get<Company[]>(this.apiCompanyRepoUrl);
  }

  createCompany(companyForCreation : CompanyForCreation) : Observable<Object>{
    return this.#http.post(this.apiCompanyRepoUrl, companyForCreation);
  }
}
