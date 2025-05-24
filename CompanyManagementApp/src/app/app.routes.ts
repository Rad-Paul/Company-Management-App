import { Routes } from '@angular/router';
import { authRoutes } from './features/authentication/auth.routes';
import { CompaniesComponent } from './features/companies/companies.component';
import { Authenticated } from './features/authentication/req/authGuard';
import { EmployeesComponent } from './features/companies/employees/employees.component';

export const routes: Routes = [
    {
        path: 'companies/:companyId/employees',
        component: EmployeesComponent,
        canActivate: [Authenticated]
    },
    {
        path: 'auth',
        children: authRoutes
    },
    {
        path: 'companies',
        component: CompaniesComponent,
        canActivate: [Authenticated],
    },
    {
        path: '',
        redirectTo: 'auth/login',
        pathMatch: 'full'
    }
];
