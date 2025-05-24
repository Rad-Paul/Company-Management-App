import { Component, input, InputSignal } from '@angular/core';
import { RouterLink } from '@angular/router';
import Company from '@app/shared/models/companies/company';

@Component({
  selector: 'app-company-card',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './company-card.component.html',
  styleUrl: './company-card.component.css'
})
export class CompanyCardComponent {
  company : InputSignal<Company | undefined> = input<Company>();
}
