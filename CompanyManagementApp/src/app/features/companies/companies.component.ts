import { Component, OnInit } from '@angular/core';
import { CompanyService } from '@app/services/company/company.service';
import { Observable } from 'rxjs';
import { CompanyCardComponent } from '@shared/components/company-card/company-card.component';
import Company from '@shared/models/companies/company';
import { SignalRService } from '@app/services/signalR/signal-r.service';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { NewCompanyFormModalComponent } from './components/new-company-form-modal/new-company-form-modal.component';
import { response } from 'express';


@Component({
  selector: 'app-companies',
  standalone: true,
  imports: [CompanyCardComponent, MatDialogModule],
  templateUrl: './companies.component.html',
  styleUrl: './companies.component.css'
})
export class CompaniesComponent implements OnInit {
  companiesFetched : boolean = false;
  companies : Company[] | undefined;
  $companies : Observable<Company[]> | undefined;

  constructor(private companyService : CompanyService, private signalRService : SignalRService, private dialog: MatDialog){

  }

  ngOnInit(): void {
    this.companyService.getAllCompanies().subscribe({
      next: (response) => {
        this.companies = response;
        this.companiesFetched = true;
      },
      error: (err) => {
        console.log(err);
      }
    });

    this.signalRService.hubConnection.on('NewCompanyCreated', (newCompany : Company) => {
      console.log(newCompany);
      this.companies = [newCompany, ...(this.companies ?? [])];
    });
  }

  openFormDialog(){
    const dialogRef = this.dialog.open(NewCompanyFormModalComponent, {
      width: '400px'
    })

    dialogRef.afterClosed().subscribe(result => {
      if(result)
        this.companyService.createCompany(result).subscribe(() => {
          console.log('Company Created');
      });
    })
  }

  

}
