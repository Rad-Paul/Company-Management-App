import { Route } from "@angular/router"
import { LoginFormComponent } from "./components/login-form/login-form.component"
import { RegistrationFormComponent } from "./components/registration-form/registration-form.component"
export const authRoutes : Route[] = [
    {
        path: 'login',
        component: LoginFormComponent
    },
    {
        path: 'registration',
        component: RegistrationFormComponent
    }
]